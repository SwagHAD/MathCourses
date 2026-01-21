using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MathDbContext))]
    [Migration("20260118213238_AddedSomeTableManyToMany")]
    /// <inheritdoc />
    public partial class AddedSomeTableManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Teachers_TeacherID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TeacherID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "TeacherGroups",
                columns: table => new
                {
                    FirstEntityId = table.Column<int>(type: "integer", nullable: false),
                    SecondEntityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGroups", x => new { x.FirstEntityId, x.SecondEntityId });
                    table.ForeignKey(
                        name: "FK_TeacherGroups_Groups_SecondEntityId",
                        column: x => x.SecondEntityId,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherGroups_Teachers_FirstEntityId",
                        column: x => x.FirstEntityId,
                        principalTable: "Teachers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroups_FirstEntityId_SecondEntityId",
                table: "TeacherGroups",
                columns: new[] { "FirstEntityId", "SecondEntityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroups_SecondEntityId",
                table: "TeacherGroups",
                column: "SecondEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherGroups");

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Groups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherID",
                table: "Groups",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Teachers_TeacherID",
                table: "Groups",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
