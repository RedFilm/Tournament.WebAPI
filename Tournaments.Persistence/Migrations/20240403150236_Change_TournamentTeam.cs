using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournaments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_TournamentTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentTeams",
                table: "TournamentTeams");

            migrationBuilder.AddColumn<long>(
                name: "PlayerId",
                table: "TournamentTeams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentTeams",
                table: "TournamentTeams",
                columns: new[] { "TournamentId", "TeamId", "Status", "PlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_PlayerId",
                table: "TournamentTeams",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentTeams_AspNetUsers_PlayerId",
                table: "TournamentTeams",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentTeams_AspNetUsers_PlayerId",
                table: "TournamentTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentTeams",
                table: "TournamentTeams");

            migrationBuilder.DropIndex(
                name: "IX_TournamentTeams_PlayerId",
                table: "TournamentTeams");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "TournamentTeams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentTeams",
                table: "TournamentTeams",
                columns: new[] { "TournamentId", "TeamId", "Status" });
        }
    }
}
