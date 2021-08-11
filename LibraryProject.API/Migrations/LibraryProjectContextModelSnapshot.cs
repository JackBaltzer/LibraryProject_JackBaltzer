﻿// <auto-generated />
using LibraryProject.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryProject.API.Migrations
{
    [DbContext(typeof(LibraryProjectContext))]
    partial class LibraryProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "George",
                            LastName = "Martin",
                            MiddleName = "R.R."
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "James",
                            LastName = "Corey",
                            MiddleName = "S.A."
                        });
                });

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Pages = 694,
                            Title = "A Game of Thrones"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Pages = 708,
                            Title = "A Clash of Kings"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Pages = 577,
                            Title = "Leviathan Wakes"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 2,
                            Pages = 544,
                            Title = "Babylons Ashes"
                        });
                });

            modelBuilder.Entity("LibraryProject.API.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "albert@mail.dk",
                            Password = "Test1234",
                            Role = 0,
                            Username = "Albert"
                        },
                        new
                        {
                            Id = 2,
                            Email = "benny@mail.dk",
                            Password = "Test1234",
                            Role = 1,
                            Username = "Benny"
                        });
                });

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Book", b =>
                {
                    b.HasOne("LibraryProject.API.Database.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
