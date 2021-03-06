// <auto-generated />
using System;
using FossilDigContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Project.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210429204350_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DigSiteModel.Models.DigSite", b =>
                {
                    b.Property<int>("DigSiteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("SiteLatitude")
                        .HasColumnType("float");

                    b.Property<float>("SiteLongitude")
                        .HasColumnType("float");

                    b.Property<string>("SiteName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DigSiteID");

                    b.HasIndex("UserID");

                    b.ToTable("DigSites");
                });

            modelBuilder.Entity("FossilModel.Models.Fossil", b =>
                {
                    b.Property<int>("FossilID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DigSiteID")
                        .HasColumnType("int");

                    b.Property<string>("FossilName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FossilSpecies")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("MuseumID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FossilID");

                    b.HasIndex("DigSiteID");

                    b.HasIndex("MuseumID");

                    b.HasIndex("UserID");

                    b.ToTable("Fossils");
                });

            modelBuilder.Entity("MuseumModel.Models.Museum", b =>
                {
                    b.Property<int>("MuseumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("MuseumLatitude")
                        .HasColumnType("float");

                    b.Property<float>("MuseumLongitude")
                        .HasColumnType("float");

                    b.Property<string>("MuseumName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("MuseumID");

                    b.HasIndex("UserID");

                    b.ToTable("Museums");
                });

            modelBuilder.Entity("UserModel.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DigSiteModel.Models.DigSite", b =>
                {
                    b.HasOne("UserModel.Models.User", "AddedBy")
                        .WithMany("DigSitesCreated")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FossilModel.Models.Fossil", b =>
                {
                    b.HasOne("DigSiteModel.Models.DigSite", "UnearthedAt")
                        .WithMany("FossilsUncovered")
                        .HasForeignKey("DigSiteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuseumModel.Models.Museum", "LocatedAt")
                        .WithMany("FossilsOwned")
                        .HasForeignKey("MuseumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel.Models.User", "AddedBy")
                        .WithMany("FossilsCreated")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MuseumModel.Models.Museum", b =>
                {
                    b.HasOne("UserModel.Models.User", "AddedBy")
                        .WithMany("MuseumsCreated")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
