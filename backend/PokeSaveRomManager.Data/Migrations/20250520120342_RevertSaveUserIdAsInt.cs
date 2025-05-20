using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertSaveUserIdAsInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saves_Users_UserId1",
                table: "Saves");

            migrationBuilder.DropIndex(
                name: "IX_Saves_UserId1",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Saves");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Saves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_UserId",
                table: "Saves",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saves_Users_UserId",
                table: "Saves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saves_Users_UserId",
                table: "Saves");

            migrationBuilder.DropIndex(
                name: "IX_Saves_UserId",
                table: "Saves");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Saves",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Saves",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Saves_UserId1",
                table: "Saves",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Saves_Users_UserId1",
                table: "Saves",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
