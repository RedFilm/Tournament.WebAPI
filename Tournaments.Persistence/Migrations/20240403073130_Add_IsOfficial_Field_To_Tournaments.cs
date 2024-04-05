using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournaments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsOfficial_Field_To_Tournaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOfficial",
                table: "Tournaments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOfficial",
                table: "Tournaments");
        }
    }
}
