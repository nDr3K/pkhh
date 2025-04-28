using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPokemonRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_Forms_FormId",
                table: "PokemonInstances");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "MoveLearning");

            migrationBuilder.CreateTable(
                name: "PokemonForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsRegional = table.Column<bool>(type: "boolean", nullable: false),
                    IsMega = table.Column<bool>(type: "boolean", nullable: false),
                    IsGigantamax = table.Column<bool>(type: "boolean", nullable: false),
                    FormOrder = table.Column<int>(type: "integer", nullable: true),
                    Type1Id = table.Column<int>(type: "integer", nullable: false),
                    Type2Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonForms_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonForms_Types_Type1Id",
                        column: x => x.Type1Id,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonForms_Types_Type2Id",
                        column: x => x.Type2Id,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    GameInMoveId = table.Column<int>(type: "integer", nullable: false),
                    MethodId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    TMNumber = table.Column<string>(type: "text", nullable: true),
                    IsTutor = table.Column<bool>(type: "boolean", nullable: false),
                    IsEggMove = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonMove_GameMoves_GameInMoveId",
                        column: x => x.GameInMoveId,
                        principalTable: "GameMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonMove_MoveLearningMethods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "MoveLearningMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonMove_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForms_PokemonId",
                table: "PokemonForms",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForms_Type1Id",
                table: "PokemonForms",
                column: "Type1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForms_Type2Id",
                table: "PokemonForms",
                column: "Type2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMove_GameInMoveId",
                table: "PokemonMove",
                column: "GameInMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMove_MethodId",
                table: "PokemonMove",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMove_PokemonId",
                table: "PokemonMove",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances",
                column: "FormId",
                principalTable: "PokemonForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances");

            migrationBuilder.DropTable(
                name: "PokemonForms");

            migrationBuilder.DropTable(
                name: "PokemonMove");

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    Type1Id = table.Column<int>(type: "integer", nullable: false),
                    Type2Id = table.Column<int>(type: "integer", nullable: true),
                    FormOrder = table.Column<int>(type: "integer", nullable: true),
                    IsGigantamax = table.Column<bool>(type: "boolean", nullable: false),
                    IsMega = table.Column<bool>(type: "boolean", nullable: false),
                    IsRegional = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forms_Types_Type1Id",
                        column: x => x.Type1Id,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Forms_Types_Type2Id",
                        column: x => x.Type2Id,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoveLearning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameInMoveId = table.Column<int>(type: "integer", nullable: false),
                    MethodId = table.Column<int>(type: "integer", nullable: false),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    IsEggMove = table.Column<bool>(type: "boolean", nullable: false),
                    IsTutor = table.Column<bool>(type: "boolean", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    TMNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveLearning_GameMoves_GameInMoveId",
                        column: x => x.GameInMoveId,
                        principalTable: "GameMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearning_MoveLearningMethods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "MoveLearningMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearning_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forms_PokemonId",
                table: "Forms",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_Type1Id",
                table: "Forms",
                column: "Type1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_Type2Id",
                table: "Forms",
                column: "Type2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_GameInMoveId",
                table: "MoveLearning",
                column: "GameInMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_MethodId",
                table: "MoveLearning",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_PokemonId",
                table: "MoveLearning",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_Forms_FormId",
                table: "PokemonInstances",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");
        }
    }
}
