using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Generation = table.Column<int>(type: "integer", nullable: false),
                    Official = table.Column<bool>(type: "boolean", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveLearningMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearningMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Natures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IncreasedStatId = table.Column<int>(type: "integer", nullable: true),
                    DecreasedStatId = table.Column<int>(type: "integer", nullable: true),
                    StatId = table.Column<int>(type: "integer", nullable: true),
                    StatId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Natures_Stats_DecreasedStatId",
                        column: x => x.DecreasedStatId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Natures_Stats_IncreasedStatId",
                        column: x => x.IncreasedStatId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Natures_Stats_StatId",
                        column: x => x.StatId,
                        principalTable: "Stats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Natures_Stats_StatId1",
                        column: x => x.StatId1,
                        principalTable: "Stats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Power = table.Column<int>(type: "integer", nullable: true),
                    Accuracy = table.Column<int>(type: "integer", nullable: true),
                    PP = table.Column<int>(type: "integer", nullable: true),
                    Effect = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moves_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DexNumber = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    HP = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    SpAttack = table.Column<int>(type: "integer", nullable: false),
                    SpDefense = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Type1Id = table.Column<int>(type: "integer", nullable: false),
                    Type2Id = table.Column<int>(type: "integer", nullable: true),
                    FormName = table.Column<string>(type: "text", nullable: false),
                    IsRegionalForm = table.Column<bool>(type: "boolean", nullable: false),
                    IsMega = table.Column<bool>(type: "boolean", nullable: false),
                    IsGigantamax = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemon_Types_Type1Id",
                        column: x => x.Type1Id,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Types_Type2Id",
                        column: x => x.Type2Id,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "Forms",
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
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false),
                    MethodId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    TMNumber = table.Column<string>(type: "text", nullable: false),
                    IsTutor = table.Column<bool>(type: "boolean", nullable: false),
                    IsEggMove = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveLearning_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearning_MoveLearningMethods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "MoveLearningMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearning_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearning_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    AbilityId = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    AbilitySlot = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    FormId = table.Column<int>(type: "integer", nullable: true),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Shiny = table.Column<bool>(type: "boolean", nullable: false),
                    NatureId = table.Column<int>(type: "integer", nullable: true),
                    HeldItemId = table.Column<int>(type: "integer", nullable: true),
                    AbilityId = table.Column<int>(type: "integer", nullable: true),
                    Move1Id = table.Column<int>(type: "integer", nullable: true),
                    Move2Id = table.Column<int>(type: "integer", nullable: true),
                    Move3Id = table.Column<int>(type: "integer", nullable: true),
                    Move4Id = table.Column<int>(type: "integer", nullable: true),
                    IVHP = table.Column<int>(type: "integer", nullable: true),
                    IVAttack = table.Column<int>(type: "integer", nullable: true),
                    IVDefense = table.Column<int>(type: "integer", nullable: true),
                    IVSpAttack = table.Column<int>(type: "integer", nullable: true),
                    IVSpDefense = table.Column<int>(type: "integer", nullable: true),
                    IVSpeed = table.Column<int>(type: "integer", nullable: true),
                    EVHP = table.Column<int>(type: "integer", nullable: true),
                    EVAttack = table.Column<int>(type: "integer", nullable: true),
                    EVDefense = table.Column<int>(type: "integer", nullable: true),
                    EVSpAttack = table.Column<int>(type: "integer", nullable: true),
                    EVSpDefense = table.Column<int>(type: "integer", nullable: true),
                    EVSpeed = table.Column<int>(type: "integer", nullable: true),
                    OriginalTrainerName = table.Column<string>(type: "text", nullable: false),
                    OriginalTrainerId = table.Column<string>(type: "text", nullable: false),
                    MetLevel = table.Column<int>(type: "integer", nullable: true),
                    MetLocation = table.Column<string>(type: "text", nullable: false),
                    Ribbons = table.Column<string>(type: "text", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: true),
                    MoveId1 = table.Column<int>(type: "integer", nullable: true),
                    MoveId2 = table.Column<int>(type: "integer", nullable: true),
                    MoveId3 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Items_HeldItemId",
                        column: x => x.HeldItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_Move1Id",
                        column: x => x.Move1Id,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_Move2Id",
                        column: x => x.Move2Id,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_Move3Id",
                        column: x => x.Move3Id,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_Move4Id",
                        column: x => x.Move4Id,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_MoveId1",
                        column: x => x.MoveId1,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_MoveId2",
                        column: x => x.MoveId2,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Moves_MoveId3",
                        column: x => x.MoveId3,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Natures_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Natures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonInstances_Users_UserId",
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
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    PokemonInstanceId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_MoveLearning_GameId",
                table: "MoveLearning",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_MethodId",
                table: "MoveLearning",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_MoveId",
                table: "MoveLearning",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_PokemonId",
                table: "MoveLearning",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_CategoryId",
                table: "Moves",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_TypeId",
                table: "Moves",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_DecreasedStatId",
                table: "Natures",
                column: "DecreasedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_IncreasedStatId",
                table: "Natures",
                column: "IncreasedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_StatId",
                table: "Natures",
                column: "StatId");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_StatId1",
                table: "Natures",
                column: "StatId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_GameId",
                table: "Pokemon",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Type1Id",
                table: "Pokemon",
                column: "Type1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Type2Id",
                table: "Pokemon",
                column: "Type2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_AbilityId",
                table: "PokemonInstances",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_FormId",
                table: "PokemonInstances",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_GameId",
                table: "PokemonInstances",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_HeldItemId",
                table: "PokemonInstances",
                column: "HeldItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_Move1Id",
                table: "PokemonInstances",
                column: "Move1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_Move2Id",
                table: "PokemonInstances",
                column: "Move2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_Move3Id",
                table: "PokemonInstances",
                column: "Move3Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_Move4Id",
                table: "PokemonInstances",
                column: "Move4Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_MoveId",
                table: "PokemonInstances",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_MoveId1",
                table: "PokemonInstances",
                column: "MoveId1");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_MoveId2",
                table: "PokemonInstances",
                column: "MoveId2");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_MoveId3",
                table: "PokemonInstances",
                column: "MoveId3");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_NatureId",
                table: "PokemonInstances",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_PokemonId",
                table: "PokemonInstances",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_UserId",
                table: "PokemonInstances",
                column: "UserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxSlots");

            migrationBuilder.DropTable(
                name: "MoveLearning");

            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "MoveLearningMethods");

            migrationBuilder.DropTable(
                name: "PokemonInstances");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Natures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
