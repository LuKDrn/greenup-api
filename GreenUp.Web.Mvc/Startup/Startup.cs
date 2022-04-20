using GreenUp.Application.Authentications.Tokens;
using GreenUp.Application.Users;
using GreenUp.Core;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.EntityFrameworkCore.Data.Seed;
using GreenUp.Web.Mvc.Hangfire;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text;

namespace GreenUp.Web.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:5001",
                    ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });
            services.AddAuthorization(config =>
            {
                config.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
                config.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
                config.AddPolicy("Association", policy => policy.RequireClaim("type", "Association"));
                config.AddPolicy("Company", policy => policy.RequireClaim("type", "Company"));
            });

            if (GreenUpConsts.HangfireEnabled)
            {
                // Add Hangfire services.
                services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(Configuration.GetConnectionString("Default"))
                    .UseMemoryStorage());


                // Add the processing server as IHostedService
                services.AddHangfireServer();
            }

            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserAppService, UserAppService>();

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                });
            });

            services.AddControllers();

            //Ajout du contexte default de BDD au Projet
            services.AddDbContext<GreenUpContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GreenUp.Web.Mvc", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                            },
                        System.Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GreenUpContext context)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreenUp.Web.Mvc v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("EnableCORS");
            app.UseStatusCodePages();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },
                IsReadOnlyFunc = (DashboardContext context) => true
            });;

            app.UseEndpoints(endpoints =>
            {
                 endpoints.MapControllers();
            });
            DbInitializer.Initialize(context);
        }
    }
}
