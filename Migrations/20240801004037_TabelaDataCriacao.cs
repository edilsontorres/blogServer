using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class TabelaDataCriacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverImg",
                table: "Posts",
                type: "VARCHAR(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriatedAt",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 31, 21, 40, 37, 270, DateTimeKind.Local).AddTicks(8557));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDateUpdate",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 31, 21, 40, 37, 270, DateTimeKind.Local).AddTicks(9054));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriatedAt",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastDateUpdate",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImg",
                table: "Posts",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)");
        }
    }
}
