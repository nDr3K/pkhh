using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreSaveFileProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Badges",
                table: "Saves",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Saves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Saves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PlayTime",
                table: "Saves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "Saves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Saves",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Badges",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "PlayTime",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Saves");
        }
    }
}
