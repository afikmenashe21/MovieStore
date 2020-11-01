using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Migrations
{
    public partial class addedSuggestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Genre",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_UserId",
                table: "Genre",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_User_UserId",
                table: "Genre",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_User_UserId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_UserId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Genre");
        }
    }
}
