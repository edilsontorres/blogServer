using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoNomeTabelaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriatedAt",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDateUpdate",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 31, 21, 40, 37, 270, DateTimeKind.Local).AddTicks(9054));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDateUpdate",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 31, 21, 40, 37, 270, DateTimeKind.Local).AddTicks(9054),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriatedAt",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 31, 21, 40, 37, 270, DateTimeKind.Local).AddTicks(8557));
        }
    }
}
