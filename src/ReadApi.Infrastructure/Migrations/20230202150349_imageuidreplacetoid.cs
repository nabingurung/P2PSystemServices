using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class imageuidreplacetoid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_uid",
                table: "media",
                newName: "image_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_id",
                table: "media",
                newName: "image_uid");
        }
    }
}
