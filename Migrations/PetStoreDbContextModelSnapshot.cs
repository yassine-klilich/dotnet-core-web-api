﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracticeWebAPI.DbContexts;

#nullable disable

namespace PracticeWebAPI.Migrations
{
    [DbContext(typeof(PetStoreDbContext))]
    partial class PetStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PracticeWebAPI.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description 1",
                            Name = "Ahmad hamidi"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description 2",
                            Name = "Yassine KLILICH"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Description 3",
                            Name = "Oumaima Ibrahimi"
                        });
                });

            modelBuilder.Entity("PracticeWebAPI.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Cat",
                            Name = "Mimi",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "Dog",
                            Name = "Dodi",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Lion",
                            Name = "Alex",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "Giraffe",
                            Name = "Djilali",
                            OwnerId = 3
                        });
                });

            modelBuilder.Entity("PracticeWebAPI.Entities.Pet", b =>
                {
                    b.HasOne("PracticeWebAPI.Entities.Owner", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PracticeWebAPI.Entities.Owner", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}