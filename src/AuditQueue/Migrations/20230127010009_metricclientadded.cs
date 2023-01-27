using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditQueue.Migrations
{
    /// <inheritdoc />
    public partial class metricclientadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "violation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "metric_unit",
                table: "violation",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_id",
                table: "violation");

            migrationBuilder.DropColumn(
                name: "metric_unit",
                table: "violation");
        }
    }
}
