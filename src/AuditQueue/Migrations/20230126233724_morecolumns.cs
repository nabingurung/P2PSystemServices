using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditQueue.Migrations
{
    /// <inheritdoc />
    public partial class morecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "threshold",
                table: "violation");

            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,2)");

            migrationBuilder.AlterColumn<string>(
                name: "system_id",
                table: "violation",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "violation",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lic_number",
                table: "violation",
                type: "varchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lic_state",
                table: "violation",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "threshold_speed",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "vio_date",
                table: "violation",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lic_number",
                table: "violation");

            migrationBuilder.DropColumn(
                name: "lic_state",
                table: "violation");

            migrationBuilder.DropColumn(
                name: "threshold_speed",
                table: "violation");

            migrationBuilder.DropColumn(
                name: "vio_date",
                table: "violation");

            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violation",
                type: "numeric(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)");

            migrationBuilder.AlterColumn<string>(
                name: "system_id",
                table: "violation",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "violation",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<int>(
                name: "threshold",
                table: "violation",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
