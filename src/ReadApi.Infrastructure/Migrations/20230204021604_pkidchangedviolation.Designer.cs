// <auto-generated />
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
    [Migration("20230204021604_pkidchangedviolation")]
    partial class pkidchangedviolation
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
                    b.Property<long>("PKId")
                        .HasColumnType("bigint")
                        .HasColumnName("pk_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_id");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_date");

                    b.Property<int>("MediaStatus")
                        .HasColumnType("integer")
                        .HasColumnName("media_status");

                    b.Property<long>("VioId")
                        .HasColumnType("bigint")
                        .HasColumnName("vio_id");

                    b.HasKey("PKId")
                        .HasName("pk_media");

                    b.ToTable("media", (string)null);
                });

            modelBuilder.Entity("ReadApi.Core.Entities.Violation", b =>
                {
                    b.Property<long>("VioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("vio_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("VioId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<string>("ImageUID")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_uid");

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

                    b.Property<double>("PostedSpeed")
                        .HasColumnType("double precision")
                        .HasColumnName("posted_speed");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("system_id");

                    b.Property<double>("ThresholdSpeed")
                        .HasColumnType("double precision")
                        .HasColumnName("threshold_speed");

                    b.Property<double>("TimeTaken")
                        .HasColumnType("double precision")
                        .HasColumnName("time_taken");

                    b.Property<double>("TotalDistanceTravelled")
                        .HasColumnType("double precision")
                        .HasColumnName("total_distance_travelled");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("transaction_date");

                    b.Property<double>("VehicleSpeed")
                        .HasPrecision(10, 3)
                        .HasColumnType("double precision")
                        .HasColumnName("vehicle_speed");

                    b.Property<int>("VioStatus")
                        .HasColumnType("integer")
                        .HasColumnName("vio_status");

                    b.Property<DateTime>("Violationdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("violationdate");

                    b.Property<double>("gpsLat")
                        .HasColumnType("double precision")
                        .HasColumnName("gps_lat");

                    b.Property<double>("gpsLong")
                        .HasColumnType("double precision")
                        .HasColumnName("gps_long");

                    b.Property<string>("locationId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location_id");

                    b.HasKey("VioId")
                        .HasName("pk_violations");

                    b.ToTable("violations", (string)null);
                });

            modelBuilder.Entity("ReadApi.Core.Entities.Media", b =>
                {
                    b.HasOne("ReadApi.Core.Entities.Violation", "Violation")
                        .WithMany("Medias")
                        .HasForeignKey("PKId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_media_violations_violation_temp_id");

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
