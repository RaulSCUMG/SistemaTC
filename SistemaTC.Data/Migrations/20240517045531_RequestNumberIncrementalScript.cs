using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaTC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RequestNumberIncrementalScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE `Request` MODIFY COLUMN `Number` int AUTO_INCREMENT UNIQUE;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE `Request` MODIFY COLUMN `Number` int;");
        }
    }
}
