using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using dwCheckApi.DatabaseContexts;

namespace dwCheckApi.Migrations
{
    [DbContext(typeof(DwContext))]
    partial class DwContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });
        }
    }
}
