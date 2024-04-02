using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tournaments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBracket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BracketId",
                table: "Tournaments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BracketId",
                table: "Stages",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brackets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TournamentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brackets_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stages_BracketId",
                table: "Stages",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_TournamentId",
                table: "Brackets",
                column: "TournamentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Brackets_BracketId",
                table: "Stages",
                column: "BracketId",
                principalTable: "Brackets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Brackets_BracketId",
                table: "Stages");

            migrationBuilder.DropTable(
                name: "Brackets");

            migrationBuilder.DropIndex(
                name: "IX_Stages_BracketId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "BracketId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "BracketId",
                table: "Stages");
        }
    }
}
