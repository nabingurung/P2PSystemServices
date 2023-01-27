using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditQueue.Migrations
{
    /// <inheritdoc />
    public partial class coordinatesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "vio_date",
                table: "violation",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "travel_distance",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "trans_date",
                table: "violation",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "threshold_speed",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "violation",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "violation",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "lic_state",
                table: "violation",
                type: "varchar(5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "lic_number",
                table: "violation",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<string>(
                name: "gps_coordinates",
                table: "violation",
                type: "varchar(60)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gps_coordinates",
                table: "violation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "vio_date",
                table: "violation",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)")
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "travel_distance",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)")
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "trans_date",
                table: "violation",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "threshold_speed",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)")
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "violation",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "violation",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "lic_state",
                table: "violation",
                type: "varchar(5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "lic_number",
                table: "violation",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)")
                .OldAnnotation("Relational:ColumnOrder", 2);
        }
    }
}
