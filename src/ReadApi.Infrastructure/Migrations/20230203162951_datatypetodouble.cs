using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datatypetodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "violations",
                newName: "vio_status");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "media",
                newName: "media_status");

            migrationBuilder.AlterColumn<double>(
                name: "vehicle_speed",
                table: "violations",
                type: "double precision",
                precision: 10,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,3)",
                oldPrecision: 10,
                oldScale: 3);

            migrationBuilder.AlterColumn<double>(
                name: "total_distance_travelled",
                table: "violations",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "image_uid",
                table: "violations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "time_taken",
                table: "violations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_uid",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "time_taken",
                table: "violations");

            migrationBuilder.RenameColumn(
                name: "vio_status",
                table: "violations",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "media_status",
                table: "media",
                newName: "status");

            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violations",
                type: "numeric(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 10,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_distance_travelled",
                table: "violations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
