using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasctrure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFieldInPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissions_sys_ObjectTypes_ObjecType",
                table: "permissions");

            migrationBuilder.RenameColumn(
                name: "ObjecType",
                table: "permissions",
                newName: "ObjectType");

            migrationBuilder.RenameIndex(
                name: "IX_permissions_ObjecType_ActionType",
                table: "permissions",
                newName: "IX_permissions_ObjectType_ActionType");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_sys_ObjectTypes_ObjectType",
                table: "permissions",
                column: "ObjectType",
                principalTable: "sys_ObjectTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissions_sys_ObjectTypes_ObjectType",
                table: "permissions");

            migrationBuilder.RenameColumn(
                name: "ObjectType",
                table: "permissions",
                newName: "ObjecType");

            migrationBuilder.RenameIndex(
                name: "IX_permissions_ObjectType_ActionType",
                table: "permissions",
                newName: "IX_permissions_ObjecType_ActionType");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_sys_ObjectTypes_ObjecType",
                table: "permissions",
                column: "ObjecType",
                principalTable: "sys_ObjectTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
