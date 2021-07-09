﻿// <auto-generated />
using System;
using GreenUp.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GreenUp.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(GreenUpContext))]
    [Migration("20210705081948_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssociationLocation", b =>
                {
                    b.Property<int>("AdressesId")
                        .HasColumnType("int");

                    b.Property<Guid>("AssociationsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdressesId", "AssociationsId");

                    b.HasIndex("AssociationsId");

                    b.ToTable("AssociationLocation");
                });

            modelBuilder.Entity("CompanyLocation", b =>
                {
                    b.Property<int>("AdressesId")
                        .HasColumnType("int");

                    b.Property<Guid>("CompaniesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdressesId", "CompaniesId");

                    b.HasIndex("CompaniesId");

                    b.ToTable("CompanyLocation");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Associations.Models.Association", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Siren")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Associations");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Siren")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Missions.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("AssociationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsInGroup")
                        .HasColumnType("bit");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("int");

                    b.Property<int>("RewardValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssociationId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AdressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

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

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MissionUser", b =>
                {
                    b.Property<int>("MissionsId")
                        .HasColumnType("int");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MissionsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("MissionUser");
                });

            modelBuilder.Entity("AssociationLocation", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Locations.Models.Location", null)
                        .WithMany()
                        .HasForeignKey("AdressesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenUp.Core.Business.Associations.Models.Association", null)
                        .WithMany()
                        .HasForeignKey("AssociationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyLocation", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Locations.Models.Location", null)
                        .WithMany()
                        .HasForeignKey("AdressesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenUp.Core.Business.Companies.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompaniesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenUp.Core.Business.Associations.Models.Association", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Users.Models.Role", "Role")
                        .WithMany("Assocations")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Company", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Users.Models.Role", "Role")
                        .WithMany("Companies")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
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

            modelBuilder.Entity("GreenUp.Core.Business.Missions.Models.Mission", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Associations.Models.Association", "Association")
                        .WithMany("Missions")
                        .HasForeignKey("AssociationId");

                    b.HasOne("GreenUp.Core.Business.Locations.Models.Location", "Place")
                        .WithMany("Missions")
                        .HasForeignKey("PlaceId");

                    b.Navigation("Association");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.User", b =>
                {
                    b.HasOne("GreenUp.Core.Business.Locations.Models.Location", "Adress")
                        .WithMany("Users")
                        .HasForeignKey("AdressId");

                    b.HasOne("GreenUp.Core.Business.Users.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Adress");

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
                    b.Navigation("Missions");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Companies.Models.Company", b =>
                {
                    b.Navigation("Rewards");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Locations.Models.Location", b =>
                {
                    b.Navigation("Missions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("GreenUp.Core.Business.Users.Models.Role", b =>
                {
                    b.Navigation("Assocations");

                    b.Navigation("Companies");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}