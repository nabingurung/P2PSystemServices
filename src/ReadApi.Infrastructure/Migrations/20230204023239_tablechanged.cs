using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tablechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media");

            migrationBuilder.DropPrimaryKey(
                name: "pk_violations",
                table: "violations");

            migrationBuilder.RenameTable(
                name: "violations",
                newName: "violation");

            migrationBuilder.RenameColumn(
                name: "pk_id",
                table: "media",
                newName: "media_id");

            migrationBuilder.AlterColumn<long>(
                name: "media_id",
                table: "media",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_violation",
                table: "violation",
                column: "vio_id");

            migrationBuilder.CreateIndex(
                name: "ix_media_vio_id",
                table: "media",
                column: "vio_id");

            migrationBuilder.AddForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media",
                column: "vio_id",
                principalTable: "violation",
                principalColumn: "vio_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media");

            migrationBuilder.DropIndex(
                name: "ix_media_vio_id",
                table: "media");

            migrationBuilder.DropPrimaryKey(
                name: "pk_violation",
                table: "violation");

            migrationBuilder.RenameTable(
                name: "violation",
                newName: "violations");

            migrationBuilder.RenameColumn(
                name: "media_id",
                table: "media",
                newName: "pk_id");

            migrationBuilder.AlterColumn<long>(
                name: "pk_id",
                table: "media",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_violations",
                table: "violations",
                column: "vio_id");

            migrationBuilder.AddForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media",
                column: "pk_id",
                principalTable: "violations",
                principalColumn: "vio_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
