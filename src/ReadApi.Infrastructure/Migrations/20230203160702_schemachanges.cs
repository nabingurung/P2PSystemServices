using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReadApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class schemachanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_media_violations_violation_id",
                table: "media");

            migrationBuilder.DropPrimaryKey(
                name: "pk_media",
                table: "media");

            migrationBuilder.DropIndex(
                name: "ix_media_violation_id",
                table: "media");

            migrationBuilder.DropColumn(
                name: "id",
                table: "media");

            migrationBuilder.RenameColumn(
                name: "gps_coordinates",
                table: "violations",
                newName: "location_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "violations",
                newName: "pk_id");

            migrationBuilder.RenameColumn(
                name: "violation_id",
                table: "media",
                newName: "pk_id");

            migrationBuilder.AlterColumn<double>(
                name: "threshold_speed",
                table: "violations",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "gps_lat",
                table: "violations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "gps_long",
                table: "violations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "posted_speed",
                table: "violations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

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

            migrationBuilder.AddPrimaryKey(
                name: "pk_media",
                table: "media",
                column: "pk_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_media_violations_violation_temp_id",
                table: "media");

            migrationBuilder.DropPrimaryKey(
                name: "pk_media",
                table: "media");

            migrationBuilder.DropIndex(
                name: "ix_media_violation_pk_id",
                table: "media");

            migrationBuilder.DropColumn(
                name: "gps_lat",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "gps_long",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "posted_speed",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "violation_pk_id",
                table: "media");

            migrationBuilder.RenameColumn(
                name: "location_id",
                table: "violations",
                newName: "gps_coordinates");

            migrationBuilder.RenameColumn(
                name: "pk_id",
                table: "violations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "pk_id",
                table: "media",
                newName: "violation_id");

            migrationBuilder.AlterColumn<int>(
                name: "threshold_speed",
                table: "violations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<long>(
                name: "violation_id",
                table: "media",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "media",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_media",
                table: "media",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_media_violation_id",
                table: "media",
                column: "violation_id");

            migrationBuilder.AddForeignKey(
                name: "fk_media_violations_violation_id",
                table: "media",
                column: "violation_id",
                principalTable: "violations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
