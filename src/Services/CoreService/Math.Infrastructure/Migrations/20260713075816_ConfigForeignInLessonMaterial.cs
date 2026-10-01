using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigForeignInLessonMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonMaterials_Lessons_LessonId",
                table: "LessonMaterials");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonMaterials_Lessons_LessonId",
                table: "LessonMaterials",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonMaterials_Lessons_LessonId",
                table: "LessonMaterials");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonMaterials_Lessons_LessonId",
                table: "LessonMaterials",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
