﻿// <auto-generated />
using Countries_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Countries_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220221231735_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Countries_API.Data.Models.Country", b =>
                {
                    b.Property<string>("ISOCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CapitalCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContinentCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ISOCode");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Countries_API.Data.Models.Language", b =>
                {
                    b.Property<string>("IsoCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryISOCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IsoCode");

                    b.HasIndex("CountryISOCode");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Countries_API.Data.Models.Language", b =>
                {
                    b.HasOne("Countries_API.Data.Models.Country", null)
                        .WithMany("Languages")
                        .HasForeignKey("CountryISOCode");
                });

            modelBuilder.Entity("Countries_API.Data.Models.Country", b =>
                {
                    b.Navigation("Languages");
                });
#pragma warning restore 612, 618
        }
    }
}
