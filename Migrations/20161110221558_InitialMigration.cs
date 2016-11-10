using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dwCheckApi.Migrations
{
    public partial class InitialMigration : Migration
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
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
