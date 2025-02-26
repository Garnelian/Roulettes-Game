using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoulettesGame.Migrations
{
    /// <inheritdoc />
    public partial class AddAmmountWonAndResultNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultNumber",
                table: "Rounds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountWon",
                table: "Bets",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultNumber",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "AmountWon",
                table: "Bets");
        }
    }
}
