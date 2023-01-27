using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuditQueue.Migrations
{
    /// <inheritdoc />
    public partial class mediaadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "violation",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "travel_distance",
                table: "violation",
                type: "numeric(10,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    imageuid = table.Column<string>(name: "image_uid", type: "varchar(50)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false),
                    transdate = table.Column<DateTime>(name: "trans_date", type: "timestamp with time zone", nullable: false),
                    ViolationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_media_violation_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "violation",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_media_ViolationId",
                table: "media",
                column: "ViolationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropColumn(
                name: "travel_distance",
                table: "violation");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "violation",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
