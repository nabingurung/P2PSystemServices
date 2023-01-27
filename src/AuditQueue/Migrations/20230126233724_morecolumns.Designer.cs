﻿// <auto-generated />
using System;
using AuditQueue.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuditQueue.Migrations
{
    [DbContext(typeof(ViolationDbContext))]
    [Migration("20230126233724_morecolumns")]
    partial class morecolumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuditQueue.DbModels.Violation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasColumnName("lic_number");

                    b.Property<string>("LicenseState")
                        .IsRequired()
                        .HasColumnType("varchar(5)")
                        .HasColumnName("lic_state");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("location");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("system_id");

                    b.Property<decimal>("ThresholdSpeed")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("threshold_speed");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("trans_date");

                    b.Property<decimal>("VehicleSpeed")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("vehicle_speed");

                    b.Property<DateTime>("Violationdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("vio_date");

                    b.HasKey("Id");

                    b.ToTable("violation");
                });
#pragma warning restore 612, 618
        }
    }
}