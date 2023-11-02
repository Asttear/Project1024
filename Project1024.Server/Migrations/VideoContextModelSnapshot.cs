﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project1024.Server.Data;

#nullable disable

namespace Project1024.Server.Migrations
{
    [DbContext(typeof(VideoContext))]
    partial class VideoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Project1024.Server.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("CoverUrl")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("varchar(511)")
                        .HasColumnName("cover_url");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)")
                        .HasColumnName("duration");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.Property<DateTimeOffset>("UploadTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("upload_time");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("varchar(511)")
                        .HasColumnName("url");

                    b.Property<int>("Views")
                        .HasColumnType("int")
                        .HasColumnName("views");

                    b.HasKey("Id")
                        .HasName("pk_video");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_video_category_id");

                    b.ToTable("video", (string)null);
                });

            modelBuilder.Entity("Project1024.Server.Models.VideoCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_video_category");

                    b.ToTable("video_category", (string)null);
                });

            modelBuilder.Entity("Project1024.Server.Models.Video", b =>
                {
                    b.HasOne("Project1024.Server.Models.VideoCategory", "Category")
                        .WithMany("Videos")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_video_video_category_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Project1024.Server.Models.VideoCategory", b =>
                {
                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
