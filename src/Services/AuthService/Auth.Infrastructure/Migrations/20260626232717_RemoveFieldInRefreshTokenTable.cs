using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasctrure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldInRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplacedByTokenHash",
                table: "refresh_token_sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplacedByTokenHash",
                table: "refresh_token_sessions",
                type: "text",
                nullable: true);
        }
    }
}
