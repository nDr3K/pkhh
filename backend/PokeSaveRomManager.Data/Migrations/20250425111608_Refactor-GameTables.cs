using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorGameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_MoveGames_MoveGameId",
                table: "MoveLearning");

            migrationBuilder.DropTable(
                name: "AbilityGames");

            migrationBuilder.RenameColumn(
                name: "GameTypeId",
                table: "TypeGames",
                newName: "TypeInGameId");

            migrationBuilder.RenameColumn(
                name: "MoveGameId",
                table: "MoveLearning",
                newName: "GameInMoveId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveLearning_MoveGameId",
                table: "MoveLearning",
                newName: "IX_MoveLearning_GameInMoveId");

            migrationBuilder.RenameColumn(
                name: "MoveGameId",
                table: "MoveGames",
                newName: "MoveInGameId");

            migrationBuilder.CreateTable(
                name: "GameAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AbilityId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    AbilityInGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameAbilities_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameAbilities_AbilityId",
                table: "GameAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_GameAbilities_GameId",
                table: "GameAbilities",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_MoveGames_GameInMoveId",
                table: "MoveLearning",
                column: "GameInMoveId",
                principalTable: "MoveGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_MoveGames_GameInMoveId",
                table: "MoveLearning");

            migrationBuilder.DropTable(
                name: "GameAbilities");

            migrationBuilder.RenameColumn(
                name: "TypeInGameId",
                table: "TypeGames",
                newName: "GameTypeId");

            migrationBuilder.RenameColumn(
                name: "GameInMoveId",
                table: "MoveLearning",
                newName: "MoveGameId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveLearning_GameInMoveId",
                table: "MoveLearning",
                newName: "IX_MoveLearning_MoveGameId");

            migrationBuilder.RenameColumn(
                name: "MoveInGameId",
                table: "MoveGames",
                newName: "MoveGameId");

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

            migrationBuilder.CreateIndex(
                name: "IX_AbilityGames_AbilityId",
                table: "AbilityGames",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityGames_GameId",
                table: "AbilityGames",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_MoveGames_MoveGameId",
                table: "MoveLearning",
                column: "MoveGameId",
                principalTable: "MoveGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
