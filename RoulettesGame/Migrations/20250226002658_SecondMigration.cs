using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoulettesGame.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Roulletes_RoulletteId",
                table: "Rounds");

            migrationBuilder.DropTable(
                name: "Roulletes");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Bets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Bets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Bets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roulettes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roulettes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Roulettes_RoulletteId",
                table: "Rounds",
                column: "RoulletteId",
                principalTable: "Roulettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Roulettes_RoulletteId",
                table: "Rounds");

            migrationBuilder.DropTable(
                name: "Roulettes");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Bets");

            migrationBuilder.CreateTable(
                name: "Roulletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roulletes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Roulletes_RoulletteId",
                table: "Rounds",
                column: "RoulletteId",
                principalTable: "Roulletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
