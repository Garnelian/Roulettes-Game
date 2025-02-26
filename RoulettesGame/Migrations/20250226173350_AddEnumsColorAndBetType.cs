using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoulettesGame.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumsColorAndBetType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "AmountWon",
                table: "Bets",
                newName: "TotalEarnings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificatedAt",
                table: "Bets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "BetType",
                table: "Bets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorBet",
                table: "Bets",
                type: "int",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetType",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "ColorBet",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "TotalEarnings",
                table: "Bets",
                newName: "AmountWon");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificatedAt",
                table: "Bets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Bets",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
