using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using dwCheckApi.DatabaseContexts;

namespace dwCheckApi.Migrations
{
    [DbContext(typeof(DwContext))]
    [Migration("20161114223829_AddedListOfCharactersToBookEntity")]
    partial class AddedListOfCharactersToBookEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

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

            modelBuilder.Entity("dwCheckApi.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookId");

                    b.Property<string>("CharacterName");

                    b.HasKey("CharacterId");

                    b.HasIndex("BookId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("dwCheckApi.Models.Character", b =>
                {
                    b.HasOne("dwCheckApi.Models.Book", "Book")
                        .WithMany("Characters")
                        .HasForeignKey("BookId");
                });
        }
    }
}
