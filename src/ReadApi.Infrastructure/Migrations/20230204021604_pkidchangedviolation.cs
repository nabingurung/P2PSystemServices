using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pkidchangedviolation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media");

            migrationBuilder.DropIndex(
                name: "ix_media_violation_pk_id",
                table: "media");

            migrationBuilder.DropColumn(
                name: "violation_pk_id",
                table: "media");

            migrationBuilder.RenameColumn(
                name: "pk_id",
                table: "violations",
                newName: "vio_id");

            migrationBuilder.AlterColumn<long>(
                name: "pk_id",
                table: "media",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "vio_id",
                table: "media",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media",
                column: "pk_id",
                principalTable: "violations",
                principalColumn: "vio_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media");

            migrationBuilder.DropColumn(
                name: "vio_id",
                table: "media");

            migrationBuilder.RenameColumn(
                name: "vio_id",
                table: "violations",
                newName: "pk_id");

            migrationBuilder.AlterColumn<long>(
                name: "pk_id",
                table: "media",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "violation_pk_id",
                table: "media",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_media_violation_pk_id",
                table: "media",
                column: "violation_pk_id");

            migrationBuilder.AddForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media",
                column: "violation_pk_id",
                principalTable: "violations",
                principalColumn: "pk_id");
        }
    }
}
