using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableFormIdForInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "PokemonInstances",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "PokemonInstances",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInstances_PokemonForms_FormId",
                table: "PokemonInstances",
                column: "FormId",
                principalTable: "PokemonForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
