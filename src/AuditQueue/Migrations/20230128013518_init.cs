using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuditQueue.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "violation",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    licstate = table.Column<string>(name: "lic_state", type: "varchar(5)", nullable: false),
                    licnumber = table.Column<string>(name: "lic_number", type: "varchar(15)", nullable: false),
                    viodate = table.Column<DateTime>(name: "vio_date", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    location = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    gpscoordinates = table.Column<string>(name: "gps_coordinates", type: "varchar(60)", nullable: false),
                    thresholdspeed = table.Column<decimal>(name: "threshold_speed", type: "numeric(10,5)", nullable: false),
                    vehiclespeed = table.Column<decimal>(name: "vehicle_speed", type: "numeric(10,5)", nullable: false),
                    traveldistance = table.Column<decimal>(name: "travel_distance", type: "numeric(10,5)", nullable: false),
                    metricunit = table.Column<string>(name: "metric_unit", type: "varchar(10)", nullable: false),
                    systemid = table.Column<string>(name: "system_id", type: "text", nullable: false),
                    transdate = table.Column<DateTime>(name: "trans_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_violation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    imageuid = table.Column<string>(name: "image_uid", type: "varchar(50)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false),
                    transdate = table.Column<DateTime>(name: "trans_date", type: "timestamp with time zone", nullable: false),
                    ViolationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_media_violation_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "violation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_media_ViolationId",
                table: "media",
                column: "ViolationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "violation");
        }
    }
}
