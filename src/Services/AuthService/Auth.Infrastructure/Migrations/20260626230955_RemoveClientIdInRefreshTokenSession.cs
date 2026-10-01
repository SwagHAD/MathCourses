using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasctrure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClientIdInRefreshTokenSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "refresh_token_sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "refresh_token_sessions",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
