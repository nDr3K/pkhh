using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Saves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_Users_UserId",
                table: "PokemonInstances");

            migrationBuilder.DropTable(
                name: "BoxSlots");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PokemonInstances",
                newName: "SaveId");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonInstances_UserId",
                table: "PokemonInstances",
                newName: "IX_PokemonInstances_SaveId");

            migrationBuilder.CreateTable(
                name: "Saves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Saves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Saves_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaveBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaveId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveBoxes_Saves_SaveId",
                        column: x => x.SaveId,
                        principalTable: "Saves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaveTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaveId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveTeams_Saves_SaveId",
                        column: x => x.SaveId,
                        principalTable: "Saves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaveBoxSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoxId = table.Column<int>(type: "integer", nullable: false),
                    PokemonInstanceId = table.Column<int>(type: "integer", nullable: false),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveBoxSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveBoxSlots_PokemonInstances_PokemonInstanceId",
                        column: x => x.PokemonInstanceId,
                        principalTable: "PokemonInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaveBoxSlots_SaveBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SaveBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaveTeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    PokemonInstanceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveTeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveTeamMembers_PokemonInstances_PokemonInstanceId",
                        column: x => x.PokemonInstanceId,
                        principalTable: "PokemonInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaveTeamMembers_SaveTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "SaveTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaveBoxes_SaveId",
                table: "SaveBoxes",
                column: "SaveId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveBoxSlots_BoxId",
                table: "SaveBoxSlots",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveBoxSlots_PokemonInstanceId",
                table: "SaveBoxSlots",
                column: "PokemonInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_GameId",
                table: "Saves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_UserId",
                table: "Saves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveTeamMembers_PokemonInstanceId",
                table: "SaveTeamMembers",
                column: "PokemonInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveTeamMembers_TeamId",
                table: "SaveTeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveTeams_SaveId",
                table: "SaveTeams",
                column: "SaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_Saves_SaveId",
                table: "PokemonInstances",
                column: "SaveId",
                principalTable: "Saves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_Saves_SaveId",
                table: "PokemonInstances");

            migrationBuilder.DropTable(
                name: "SaveBoxSlots");

            migrationBuilder.DropTable(
                name: "SaveTeamMembers");

            migrationBuilder.DropTable(
                name: "SaveBoxes");

            migrationBuilder.DropTable(
                name: "SaveTeams");

            migrationBuilder.DropTable(
                name: "Saves");

            migrationBuilder.RenameColumn(
                name: "SaveId",
                table: "PokemonInstances",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonInstances_SaveId",
                table: "PokemonInstances",
                newName: "IX_PokemonInstances_UserId");

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxes_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boxes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoxId = table.Column<int>(type: "integer", nullable: false),
                    PokemonInstanceId = table.Column<int>(type: "integer", nullable: false),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxSlots_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxSlots_PokemonInstances_PokemonInstanceId",
                        column: x => x.PokemonInstanceId,
                        principalTable: "PokemonInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonInstanceId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMembers_PokemonInstances_PokemonInstanceId",
                        column: x => x.PokemonInstanceId,
                        principalTable: "PokemonInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_GameId",
                table: "Boxes",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_UserId",
                table: "Boxes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxSlots_BoxId",
                table: "BoxSlots",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxSlots_PokemonInstanceId",
                table: "BoxSlots",
                column: "PokemonInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_PokemonInstanceId",
                table: "TeamMembers",
                column: "PokemonInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_Users_UserId",
                table: "PokemonInstances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
