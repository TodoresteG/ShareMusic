using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMusic.Data.Migrations
{
    public partial class GroupUserDeletebale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GroupUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "GroupUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GroupUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GroupUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "GroupUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
