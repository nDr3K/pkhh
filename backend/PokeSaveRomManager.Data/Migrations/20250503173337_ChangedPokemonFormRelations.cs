using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPokemonFormRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_Types_Type1Id",
                table: "Pokemon");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_Types_Type2Id",
                table: "Pokemon");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_Games_GameId",
                table: "PokemonInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances");

            migrationBuilder.DropIndex(
                name: "IX_PokemonInstances_GameId",
                table: "PokemonInstances");

            migrationBuilder.DropIndex(
                name: "IX_Pokemon_Type1Id",
                table: "Pokemon");

            migrationBuilder.DropIndex(
                name: "IX_Pokemon_Type2Id",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "MetLevel",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "MetLocation",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "OriginalTrainerId",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "OriginalTrainerName",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "Ribbons",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "FormName",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "HP",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "IsGigantamax",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "IsMega",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "IsRegionalForm",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "SpAttack",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "SpDefense",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Special",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Type1Id",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Type2Id",
                table: "Pokemon");

            migrationBuilder.RenameColumn(
                name: "FormOrder",
                table: "PokemonForms",
                newName: "Special");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "PokemonInstances",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "PokemonForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "PokemonForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HP",
                table: "PokemonForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InternalId",
                table: "PokemonForms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "PokemonForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpAttack",
                table: "PokemonForms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpDefense",
                table: "PokemonForms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "PokemonForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances",
                column: "FormId",
                principalTable: "PokemonForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "HP",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "InternalId",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "SpAttack",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "SpDefense",
                table: "PokemonForms");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "PokemonForms");

            migrationBuilder.RenameColumn(
                name: "Special",
                table: "PokemonForms",
                newName: "FormOrder");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "PokemonInstances",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "PokemonInstances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MetLevel",
                table: "PokemonInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetLocation",
                table: "PokemonInstances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalTrainerId",
                table: "PokemonInstances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalTrainerName",
                table: "PokemonInstances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ribbons",
                table: "PokemonInstances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FormName",
                table: "Pokemon",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HP",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsGigantamax",
                table: "Pokemon",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMega",
                table: "Pokemon",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegionalForm",
                table: "Pokemon",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpAttack",
                table: "Pokemon",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpDefense",
                table: "Pokemon",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Special",
                table: "Pokemon",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type1Id",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type2Id",
                table: "Pokemon",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInstances_GameId",
                table: "PokemonInstances",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Type1Id",
                table: "Pokemon",
                column: "Type1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Type2Id",
                table: "Pokemon",
                column: "Type2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_Types_Type1Id",
                table: "Pokemon",
                column: "Type1Id",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_Types_Type2Id",
                table: "Pokemon",
                column: "Type2Id",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_Games_GameId",
                table: "PokemonInstances",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances",
                column: "FormId",
                principalTable: "PokemonForms",
                principalColumn: "Id");
        }
    }
}
