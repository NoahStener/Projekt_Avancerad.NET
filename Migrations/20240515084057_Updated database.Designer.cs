﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt_Avancerad.NET.Data;

#nullable disable

namespace Projekt_Avancerad.NET.Migrations
{
    [DbContext(typeof(ProjektDbContext))]
    [Migration("20240515084057_Updated database")]
    partial class Updateddatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjektModels.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            AppointmentID = 1,
                            CustomerID = 1,
                            Description = "Conference booked for 10+ people",
                            EndDate = new DateTime(2024, 5, 20, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 5, 20, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Meeting"
                        },
                        new
                        {
                            AppointmentID = 2,
                            CustomerID = 2,
                            Description = "Conference booked for 10+ people",
                            EndDate = new DateTime(2024, 5, 24, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 5, 24, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Meeting"
                        },
                        new
                        {
                            AppointmentID = 3,
                            CustomerID = 3,
                            Description = "Conference booked for 10+ people",
                            EndDate = new DateTime(2024, 5, 25, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 5, 25, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Meeting"
                        });
                });

            modelBuilder.Entity("ProjektModels.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyID = 1,
                            CompanyName = "Solviken"
                        });
                });

            modelBuilder.Entity("ProjektModels.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerID = 1,
                            Email = "jonas@gmail.se",
                            FirstName = "Jonas",
                            LastName = "Svensson",
                            Phone = "0712345432"
                        },
                        new
                        {
                            CustomerID = 2,
                            Email = "lovisa@gmail.se",
                            FirstName = "Lovisa",
                            LastName = "Johansson",
                            Phone = "0712345444"
                        },
                        new
                        {
                            CustomerID = 3,
                            Email = "Göran@gmail.se",
                            FirstName = "Göran",
                            LastName = "Karlsson",
                            Phone = "0712345666"
                        });
                });

            modelBuilder.Entity("Projekt_Avancerad.NET.Helper.AppointmentHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("ChangeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppointmentHistories");
                });

            modelBuilder.Entity("ProjektModels.Appointment", b =>
                {
                    b.HasOne("ProjektModels.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjektModels.Customer", b =>
                {
                    b.HasOne("ProjektModels.Company", null)
                        .WithMany("Customers")
                        .HasForeignKey("CompanyID");
                });

            modelBuilder.Entity("ProjektModels.Company", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("ProjektModels.Customer", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}