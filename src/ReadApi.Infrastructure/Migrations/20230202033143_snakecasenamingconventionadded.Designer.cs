﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReadApi.Infrastructure.Peristence.EfCore;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230202033143_snakecasenamingconventionadded")]
    partial class snakecasenamingconventionadded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ReadApi.Core.Entities.Media", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("ImageUID")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_uid");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long>("ViolationId")
                        .HasColumnType("bigint")
                        .HasColumnName("violation_id");

                    b.HasKey("Id")
                        .HasName("pk_media");

                    b.HasIndex("ViolationId")
                        .HasDatabaseName("ix_media_violation_id");

                    b.ToTable("media", (string)null);
                });

            modelBuilder.Entity("ReadApi.Core.Entities.Violation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<string>("GPSCoordinates")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gps_coordinates");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("license_number");

                    b.Property<string>("LicenseState")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("license_state");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("location");

                    b.Property<string>("MetricUnitSystem")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("metric_unit_system");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("system_id");

                    b.Property<int>("ThresholdSpeed")
                        .HasColumnType("integer")
                        .HasColumnName("threshold_speed");

                    b.Property<decimal>("TotalDistanceTravelled")
                        .HasColumnType("numeric")
                        .HasColumnName("total_distance_travelled");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("transaction_date");

                    b.Property<decimal>("VehicleSpeed")
                        .HasColumnType("numeric")
                        .HasColumnName("vehicle_speed");

                    b.Property<DateTime>("Violationdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("violationdate");

                    b.HasKey("Id")
                        .HasName("pk_violations");

                    b.ToTable("violations", (string)null);
                });

            modelBuilder.Entity("ReadApi.Core.Entities.Media", b =>
                {
                    b.HasOne("ReadApi.Core.Entities.Violation", "Violation")
                        .WithMany("Medias")
                        .HasForeignKey("ViolationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_media_violations_violation_id");

                    b.Navigation("Violation");
                });

            modelBuilder.Entity("ReadApi.Core.Entities.Violation", b =>
                {
                    b.Navigation("Medias");
                });
#pragma warning restore 612, 618
        }
    }
}
