// <auto-generated />
using System;
using AuditQueue.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuditQueue.Migrations
{
    [DbContext(typeof(ViolationDbContext))]
    partial class ViolationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuditQueue.DbModels.Media", b =>
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
                        .HasColumnType("varchar(50)")
                        .HasColumnName("image_uid");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("trans_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long>("ViolationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ViolationId");

                    b.ToTable("media");
                });

            modelBuilder.Entity("AuditQueue.DbModels.Violation", b =>
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
                        .HasColumnType("varchar(60)")
                        .HasColumnName("gps_coordinates");

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

                    b.Property<string>("MetricUnitSystem")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("metric_unit");

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

                    b.Property<decimal>("TotalDistanceTravelled")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("travel_distance");

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

            modelBuilder.Entity("AuditQueue.DbModels.Media", b =>
                {
                    b.HasOne("AuditQueue.DbModels.Violation", "Violation")
                        .WithMany("Medias")
                        .HasForeignKey("ViolationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Violation");
                });

            modelBuilder.Entity("AuditQueue.DbModels.Violation", b =>
                {
                    b.Navigation("Medias");
                });
#pragma warning restore 612, 618
        }
    }
}
