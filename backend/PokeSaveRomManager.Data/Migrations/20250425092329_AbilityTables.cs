using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AbilityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Abilities");

            migrationBuilder.AddColumn<int>(
                name: "NameId",
                table: "Abilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AbilityGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AbilityId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    AbilityGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbilityGames_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbilityGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbilityNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityNames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_NameId",
                table: "Abilities",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityGames_AbilityId",
                table: "AbilityGames",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityGames_GameId",
                table: "AbilityGames",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_AbilityNames_NameId",
                table: "Abilities",
                column: "NameId",
                principalTable: "AbilityNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_AbilityNames_NameId",
                table: "Abilities");

            migrationBuilder.DropTable(
                name: "AbilityGames");

            migrationBuilder.DropTable(
                name: "AbilityNames");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_NameId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Abilities");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Abilities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
