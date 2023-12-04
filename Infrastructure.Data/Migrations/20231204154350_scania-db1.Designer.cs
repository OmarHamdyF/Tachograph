﻿// <auto-generated />
using System;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231204154350_scania-db1")]
    partial class scaniadb1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.Entities.Driver", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            Id = "Driver A",
                            FirstName = "Omar",
                            LastName = "Hamdy"
                        },
                        new
                        {
                            Id = "Driver B",
                            FirstName = "Ahmed",
                            LastName = "Hamdy"
                        });
                });

            modelBuilder.Entity("Core.Domain.Entities.TachographData", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Activity")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DriverId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EndTime")
                        .HasColumnType("text");

                    b.Property<string>("StartTime")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("TachographData");
                });

            modelBuilder.Entity("Core.Domain.Entities.TachographData", b =>
                {
                    b.HasOne("Core.Domain.Entities.Driver", "Driver")
                        .WithMany("TachographDataList")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Core.Domain.Entities.Driver", b =>
                {
                    b.Navigation("TachographDataList");
                });
#pragma warning restore 612, 618
        }
    }
}
