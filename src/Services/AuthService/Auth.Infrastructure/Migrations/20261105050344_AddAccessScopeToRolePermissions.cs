using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasctrure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessScopeToRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_roles_NormalizedName",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "roles");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "roles",
                type: "integer",
                nullable: true);

            // Уже выданные права (до появления областей) действовали на все записи — сохраняем это поведение
            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "role_permissions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "All");

            migrationBuilder.CreateIndex(
                name: "IX_roles_ParentId",
                table: "roles",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_roles_ParentId",
                table: "roles",
                column: "ParentId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_roles_ParentId",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_ParentId",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "role_permissions");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "roles",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_roles_NormalizedName",
                table: "roles",
                column: "NormalizedName",
                unique: true);
        }
    }
}
