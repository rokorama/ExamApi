﻿// <auto-generated />
using System;
using ExamApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220717170224_Add-PersonalInfo-FK-to-ResidenceInfo")]
    partial class AddPersonalInfoFKtoResidenceInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ExamApi.Models.PersonalInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PersonalNumber")
                        .HasColumnType("decimal(20,0)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("ResidenceInfoGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PersonalInfos");
                });

            modelBuilder.Entity("ExamApi.Models.ResidenceInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Flat")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResidenceInfos");
                });

            modelBuilder.Entity("ExamApi.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PersonalInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInfoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExamApi.Models.ResidenceInfo", b =>
                {
                    b.HasOne("ExamApi.Models.PersonalInfo", "PersonalInfo")
                        .WithOne("ResidenceInfo")
                        .HasForeignKey("ExamApi.Models.ResidenceInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInfo");
                });

            modelBuilder.Entity("ExamApi.Models.User", b =>
                {
                    b.HasOne("ExamApi.Models.PersonalInfo", "PersonalInfo")
                        .WithMany()
                        .HasForeignKey("PersonalInfoId");

                    b.Navigation("PersonalInfo");
                });

            modelBuilder.Entity("ExamApi.Models.PersonalInfo", b =>
                {
                    b.Navigation("ResidenceInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
