using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMusic.Data.Migrations
{
    public partial class SongMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongArtists_Songs_SongId",
                table: "SongArtists");

            migrationBuilder.CreateTable(
                name: "SongMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    SongId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongMetadata_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongMetadata_SongId",
                table: "SongMetadata",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongArtists_Songs_SongId",
                table: "SongArtists",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongArtists_Songs_SongId",
                table: "SongArtists");

            migrationBuilder.DropTable(
                name: "SongMetadata");

            migrationBuilder.AddForeignKey(
                name: "FK_SongArtists_Songs_SongId",
                table: "SongArtists",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
