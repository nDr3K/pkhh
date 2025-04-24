using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeGame_Games_GameId",
                table: "TypeGame");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeGame_Types_TypeId",
                table: "TypeGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeGame",
                table: "TypeGame");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Moves");

            migrationBuilder.RenameTable(
                name: "TypeGame",
                newName: "TypeGames");

            migrationBuilder.RenameIndex(
                name: "IX_TypeGame_TypeId",
                table: "TypeGames",
                newName: "IX_TypeGames_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeGame_GameId",
                table: "TypeGames",
                newName: "IX_TypeGames_GameId");

            migrationBuilder.AddColumn<int>(
                name: "NameId",
                table: "Moves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameTypeId",
                table: "TypeGames",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeGames",
                table: "TypeGames",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MoveNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveNames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moves_NameId",
                table: "Moves",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_MoveNames_NameId",
                table: "Moves",
                column: "NameId",
                principalTable: "MoveNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeGames_Games_GameId",
                table: "TypeGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeGames_Types_TypeId",
                table: "TypeGames",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_MoveNames_NameId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeGames_Games_GameId",
                table: "TypeGames");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeGames_Types_TypeId",
                table: "TypeGames");

            migrationBuilder.DropTable(
                name: "MoveNames");

            migrationBuilder.DropIndex(
                name: "IX_Moves_NameId",
                table: "Moves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeGames",
                table: "TypeGames");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "GameTypeId",
                table: "TypeGames");

            migrationBuilder.RenameTable(
                name: "TypeGames",
                newName: "TypeGame");

            migrationBuilder.RenameIndex(
                name: "IX_TypeGames_TypeId",
                table: "TypeGame",
                newName: "IX_TypeGame_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeGames_GameId",
                table: "TypeGame",
                newName: "IX_TypeGame_GameId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Moves",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeGame",
                table: "TypeGame",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeGame_Games_GameId",
                table: "TypeGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeGame_Types_TypeId",
                table: "TypeGame",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
