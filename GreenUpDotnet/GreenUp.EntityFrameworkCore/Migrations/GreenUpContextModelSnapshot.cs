﻿// <auto-generated />
using System;
using GreenUp.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GreenUp.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(GreenUpContext))]
    partial class GreenUpContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GreenUp.Core.Business.Associations.Models.Association", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Siren")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Associations");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Siren")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Locations.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AssociationId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssociationId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("MissionId")
                        .IsUnique()
                        .HasFilter("[MissionId] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Missions.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssociationId")
                        .HasColumnType("int");

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsInGroup")
                        .HasColumnType("bit");

                    b.Property<int>("RewardValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssociationId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MissionUser", b =>
                {
                    b.Property<int>("MissionsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("MissionsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("MissionUser");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Reward", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Companies.Models.Company", "Company")
                        .WithMany("Rewards")
                        .HasForeignKey("CompanyId");

                    b.HasOne("GreenUp.Core.Business.Users.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Locations.Models.Location", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Associations.Models.Association", "Association")
                        .WithMany("Adresses")
                        .HasForeignKey("AssociationId");

                    b.HasOne("GreenUp.Core.Business.Companies.Models.Company", "Company")
                        .WithMany("Adresses")
                        .HasForeignKey("CompanyId");

                    b.HasOne("GreenUp.Core.Business.Missions.Models.Mission", "Mission")
                        .WithOne("Place")
                        .HasForeignKey("GreenUp.Core.Business.Locations.Models.Location", "MissionId");

                    b.HasOne("GreenUp.Core.Business.Users.Models.User", "User")
                        .WithOne("Adress")
                        .HasForeignKey("GreenUp.Core.Business.Locations.Models.Location", "UserId");

                    b.Navigation("Association");

                    b.Navigation("Company");

                    b.Navigation("Mission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Missions.Models.Mission", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Associations.Models.Association", "Association")
                        .WithMany("Missions")
                        .HasForeignKey("AssociationId");

                    b.Navigation("Association");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.User", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Users.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MissionUser", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Missions.Models.Mission", null)
                        .WithMany()
                        .HasForeignKey("MissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenUp.Core.Business.Users.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenUp.Core.Business.Associations.Models.Association", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("Missions");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Company", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("Rewards");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Missions.Models.Mission", b =>
                {
                    b.Navigation("Place");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.User", b =>
                {
                    b.Navigation("Adress");
                });
#pragma warning restore 612, 618
        }
    }
}