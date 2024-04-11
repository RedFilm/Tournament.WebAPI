using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournaments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_Property_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_RecipientId",
                table: "TeamJoinRequests");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "TeamJoinRequests",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequests_RecipientId",
                table: "TeamJoinRequests",
                newName: "IX_TeamJoinRequests_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_ReceiverId",
                table: "TeamJoinRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_ReceiverId",
                table: "TeamJoinRequests");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "TeamJoinRequests",
                newName: "RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequests_ReceiverId",
                table: "TeamJoinRequests",
                newName: "IX_TeamJoinRequests_RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_RecipientId",
                table: "TeamJoinRequests",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
