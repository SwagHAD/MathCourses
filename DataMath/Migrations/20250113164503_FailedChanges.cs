using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMath.Migrations
{
    /// <inheritdoc />
    public partial class FailedChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentGroups_StudentId",
                table: "StudentGroups");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_StudentId_GroupId",
                table: "StudentGroups",
                columns: new[] { "StudentId", "GroupId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_GroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentGroups_StudentId_GroupId",
                table: "StudentGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_StudentId",
                table: "StudentGroups",
                column: "StudentId");
        }
    }
}
