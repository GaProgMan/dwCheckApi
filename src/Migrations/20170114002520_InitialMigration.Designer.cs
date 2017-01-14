using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using dwCheckApi.DatabaseContexts;

namespace src.Migrations
{
    [DbContext(typeof(DwContext))]
    [Migration("20170114002520_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("dwCheckApi.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("BookCoverImage");

                    b.Property<string>("BookCoverImageUrl");

                    b.Property<string>("BookDescription");

                    b.Property<string>("BookIsbn10");

                    b.Property<string>("BookIsbn13");

                    b.Property<string>("BookName");

                    b.Property<int>("BookOrdinal");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("dwCheckApi.Models.BookCharacter", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.HasKey("BookId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("BookCharacters");
                });

            modelBuilder.Entity("dwCheckApi.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CharacterName");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.HasKey("CharacterId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("dwCheckApi.Models.BookCharacter", b =>
                {
                    b.HasOne("dwCheckApi.Models.Book", "Book")
                        .WithMany("BookCharacter")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dwCheckApi.Models.Character", "Character")
                        .WithMany("BookCharacter")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
