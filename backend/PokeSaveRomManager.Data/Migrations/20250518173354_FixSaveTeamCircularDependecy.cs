using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSaveTeamCircularDependecy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saves_SaveTeams_TeamId",
                table: "Saves");

            migrationBuilder.DropForeignKey(
                name: "FK_SaveTeamMembers_SaveTeams_TeamId",
                table: "SaveTeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_SaveTeams_SaveId",
                table: "SaveTeams");

            migrationBuilder.DropIndex(
                name: "IX_Saves_TeamId",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SaveTeams");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SaveTeams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SaveTeams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Saves");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "SaveTeamMembers",
                newName: "PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_SaveTeamMembers_TeamId",
                table: "SaveTeamMembers",
                newName: "IX_SaveTeamMembers_PartyId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Saves",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SaveTeams_SaveId",
                table: "SaveTeams",
                column: "SaveId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SaveTeamMembers_SaveTeams_PartyId",
                table: "SaveTeamMembers",
                column: "PartyId",
                principalTable: "SaveTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaveTeamMembers_SaveTeams_PartyId",
                table: "SaveTeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_SaveTeams_SaveId",
                table: "SaveTeams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Saves");

            migrationBuilder.RenameColumn(
                name: "PartyId",
                table: "SaveTeamMembers",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_SaveTeamMembers_PartyId",
                table: "SaveTeamMembers",
                newName: "IX_SaveTeamMembers_TeamId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SaveTeams",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SaveTeams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SaveTeams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Saves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SaveTeams_SaveId",
                table: "SaveTeams",
                column: "SaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_TeamId",
                table: "Saves",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saves_SaveTeams_TeamId",
                table: "Saves",
                column: "TeamId",
                principalTable: "SaveTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaveTeamMembers_SaveTeams_TeamId",
                table: "SaveTeamMembers",
                column: "TeamId",
                principalTable: "SaveTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
