using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amora.Migrations
{
    /// <inheritdoc />
    public partial class accepted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchAccepted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    MatchedUser_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchAccepted", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_RegisterViewModel_MatchedUserId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_RegisterViewModel_UserId",
                table: "Match");

            migrationBuilder.DropTable(
                name: "MatchAccepted");

            migrationBuilder.DropIndex(
                name: "IX_Match_MatchedUserId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_UserId",
                table: "Match");
        }
    }
}
