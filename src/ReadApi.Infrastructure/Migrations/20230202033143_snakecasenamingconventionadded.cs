using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class snakecasenamingconventionadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "violations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    licensestate = table.Column<string>(name: "license_state", type: "text", nullable: false),
                    licensenumber = table.Column<string>(name: "license_number", type: "text", nullable: false),
                    violationdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    gpscoordinates = table.Column<string>(name: "gps_coordinates", type: "text", nullable: false),
                    thresholdspeed = table.Column<int>(name: "threshold_speed", type: "integer", nullable: false),
                    vehiclespeed = table.Column<decimal>(name: "vehicle_speed", type: "numeric", nullable: false),
                    totaldistancetravelled = table.Column<decimal>(name: "total_distance_travelled", type: "numeric", nullable: false),
                    metricunitsystem = table.Column<string>(name: "metric_unit_system", type: "text", nullable: false),
                    systemid = table.Column<string>(name: "system_id", type: "text", nullable: false),
                    transactiondate = table.Column<DateTime>(name: "transaction_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_violations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    imageuid = table.Column<string>(name: "image_uid", type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false),
                    lastupdateddate = table.Column<DateTime>(name: "last_updated_date", type: "timestamp with time zone", nullable: false),
                    violationid = table.Column<long>(name: "violation_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media", x => x.id);
                    table.ForeignKey(
                        name: "fk_media_violations_violation_id",
                        column: x => x.violationid,
                        principalTable: "violations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_media_violation_id",
                table: "media",
                column: "violation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "violations");
        }
    }
}
