using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dwCheckApi.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    BookCoverImage = table.Column<byte[]>(nullable: true),
                    BookCoverImageUrl = table.Column<string>(nullable: true),
                    BookDescription = table.Column<string>(nullable: true),
                    BookIsbn10 = table.Column<string>(nullable: true),
                    BookIsbn13 = table.Column<string>(nullable: true),
                    BookName = table.Column<string>(nullable: true),
                    BookOrdinal = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CharacterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "BookCharacter",
                columns: table => new
                {
                    BookCharacterId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    BookId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCharacter", x => x.BookCharacterId);
                    table.ForeignKey(
                        name: "FK_BookCharacter_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCharacter_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCharacter_BookId",
                table: "BookCharacter",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCharacter_CharacterId",
                table: "BookCharacter",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCharacter");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
