﻿// <auto-generated />
using System;
using Gallery.MemberAndImages.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gallery.Data.Migrations
{
    [DbContext(typeof(GalleryDbContext))]
    [Migration("20210827035223_MemberAndPhoto")]
    partial class MemberAndPhoto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gallery.MemberAndImages.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Gallery.MemberAndImages.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Gallery.MemberAndImages.Entities.Photo", b =>
                {
                    b.HasOne("Gallery.MemberAndImages.Entities.Member", "Member")
                        .WithMany("Photos")
                        .HasForeignKey("MemberId");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Gallery.MemberAndImages.Entities.Member", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
