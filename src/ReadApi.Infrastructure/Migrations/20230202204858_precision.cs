using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class precision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violations",
                type: "numeric(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "vehicle_speed",
                table: "violations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,3)",
                oldPrecision: 10,
                oldScale: 3);
        }
    }
}
