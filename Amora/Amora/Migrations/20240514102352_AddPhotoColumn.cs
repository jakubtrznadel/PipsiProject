using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amora.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "RegisterViewModel");
        }
    }
}
