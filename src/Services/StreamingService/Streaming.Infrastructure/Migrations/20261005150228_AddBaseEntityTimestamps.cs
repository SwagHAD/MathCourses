using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StreamToken",
                table: "StreamSessions",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "StreamPath",
                table: "StreamSessions",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RecordingPath",
                table: "StreamSessions",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "StreamSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "StreamSessions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessions_LessonId_UserId",
                table: "StreamSessions",
                columns: new[] { "LessonId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessions_StreamToken",
                table: "StreamSessions",
                column: "StreamToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StreamSessions_LessonId_UserId",
                table: "StreamSessions");

            migrationBuilder.DropIndex(
                name: "IX_StreamSessions_StreamToken",
                table: "StreamSessions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StreamSessions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StreamSessions");

            migrationBuilder.AlterColumn<string>(
                name: "StreamToken",
                table: "StreamSessions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "StreamPath",
                table: "StreamSessions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "RecordingPath",
                table: "StreamSessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
