using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditQueue.Migrations
{
    /// <inheritdoc />
    public partial class schemachange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vehiclespeed",
                table: "violation",
                newName: "vehicle_speed");

            migrationBuilder.RenameColumn(
                name: "transdate",
                table: "violation",
                newName: "trans_date");

            migrationBuilder.RenameColumn(
                name: "systemid",
                table: "violation",
                newName: "system_id");

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "violation",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violation",
                type: "numeric(4,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vehicle_speed",
                table: "violation",
                newName: "vehiclespeed");

            migrationBuilder.RenameColumn(
                name: "trans_date",
                table: "violation",
                newName: "transdate");

            migrationBuilder.RenameColumn(
                name: "system_id",
                table: "violation",
                newName: "systemid");

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "violation",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "vehiclespeed",
                table: "violation",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,2)");
        }
    }
}
