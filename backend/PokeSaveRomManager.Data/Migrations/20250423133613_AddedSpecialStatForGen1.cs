using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpecialStatForGen1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EVSpecial",
                table: "PokemonInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IVSpecial",
                table: "PokemonInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpDefense",
                table: "Pokemon",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SpAttack",
                table: "Pokemon",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Special",
                table: "Pokemon",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EVSpecial",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "IVSpecial",
                table: "PokemonInstances");

            migrationBuilder.DropColumn(
                name: "Special",
                table: "Pokemon");

            migrationBuilder.AlterColumn<int>(
                name: "SpDefense",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpAttack",
                table: "Pokemon",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
