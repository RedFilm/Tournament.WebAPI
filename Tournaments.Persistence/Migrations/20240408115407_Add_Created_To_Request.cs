using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournaments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Created_To_Request : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequest_AspNetUsers_RecipientId",
                table: "TeamJoinRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequest_AspNetUsers_SenderId",
                table: "TeamJoinRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequest_Teams_TeamId",
                table: "TeamJoinRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamJoinRequest",
                table: "TeamJoinRequest");

            migrationBuilder.RenameTable(
                name: "TeamJoinRequest",
                newName: "TeamJoinRequests");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequest_TeamId",
                table: "TeamJoinRequests",
                newName: "IX_TeamJoinRequests_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequest_SenderId",
                table: "TeamJoinRequests",
                newName: "IX_TeamJoinRequests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequest_RecipientId",
                table: "TeamJoinRequests",
                newName: "IX_TeamJoinRequests_RecipientId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "TeamJoinRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamJoinRequests",
                table: "TeamJoinRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_RecipientId",
                table: "TeamJoinRequests",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_SenderId",
                table: "TeamJoinRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequests_Teams_TeamId",
                table: "TeamJoinRequests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_RecipientId",
                table: "TeamJoinRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequests_AspNetUsers_SenderId",
                table: "TeamJoinRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamJoinRequests_Teams_TeamId",
                table: "TeamJoinRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamJoinRequests",
                table: "TeamJoinRequests");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TeamJoinRequests");

            migrationBuilder.RenameTable(
                name: "TeamJoinRequests",
                newName: "TeamJoinRequest");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequests_TeamId",
                table: "TeamJoinRequest",
                newName: "IX_TeamJoinRequest_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequests_SenderId",
                table: "TeamJoinRequest",
                newName: "IX_TeamJoinRequest_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamJoinRequests_RecipientId",
                table: "TeamJoinRequest",
                newName: "IX_TeamJoinRequest_RecipientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamJoinRequest",
                table: "TeamJoinRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequest_AspNetUsers_RecipientId",
                table: "TeamJoinRequest",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequest_AspNetUsers_SenderId",
                table: "TeamJoinRequest",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamJoinRequest_Teams_TeamId",
                table: "TeamJoinRequest",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
