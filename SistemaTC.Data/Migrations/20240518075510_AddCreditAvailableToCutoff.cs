using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaTC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditAvailableToCutoff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCreditAvailable",
                table: "CreditCutOff",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCreditAvailable",
                table: "CreditCutOff");
        }
    }
}
