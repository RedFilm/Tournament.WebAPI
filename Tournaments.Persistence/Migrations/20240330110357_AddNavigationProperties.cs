using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournaments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Brackets_BracketId",
                table: "Stages");

            migrationBuilder.AlterColumn<long>(
                name: "BracketId",
                table: "Stages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Brackets_BracketId",
                table: "Stages",
                column: "BracketId",
                principalTable: "Brackets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Brackets_BracketId",
                table: "Stages");

            migrationBuilder.AlterColumn<long>(
                name: "BracketId",
                table: "Stages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Brackets_BracketId",
                table: "Stages",
                column: "BracketId",
                principalTable: "Brackets",
                principalColumn: "Id");
        }
    }
}
