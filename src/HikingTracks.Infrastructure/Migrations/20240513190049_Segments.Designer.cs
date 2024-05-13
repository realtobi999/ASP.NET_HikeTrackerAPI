﻿// <auto-generated />
using System;
using HikingTracks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HikingTracks.Infrastructure.Migrations
{
    [DbContext(typeof(HikingTracksContext))]
    [Migration("20240513190049_Segments")]
    partial class Segments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HikingTracks.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<double>("TotalDistance")
                        .HasColumnType("double precision")
                        .HasColumnName("total_distance");

                    b.Property<int>("TotalHikes")
                        .HasColumnType("integer")
                        .HasColumnName("total_hikes");

                    b.Property<TimeSpan>("TotalMovingTime")
                        .HasColumnType("interval")
                        .HasColumnName("total_moving_time");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("username");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.Hike", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<double>("AverageSpeed")
                        .HasColumnType("double precision")
                        .HasColumnName("average_speed");

                    b.Property<string>("CoordinatesString")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("coordinates");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision")
                        .HasColumnName("distance");

                    b.Property<double>("ElevationGain")
                        .HasColumnType("double precision")
                        .HasColumnName("elevation_gain");

                    b.Property<double>("ElevationLoss")
                        .HasColumnType("double precision")
                        .HasColumnName("elevation_loss");

                    b.Property<double>("MaxSpeed")
                        .HasColumnType("double precision")
                        .HasColumnName("max_speed");

                    b.Property<TimeSpan>("MovingTime")
                        .HasColumnType("interval")
                        .HasColumnName("moving_time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("ID");

                    b.HasIndex("AccountId");

                    b.ToTable("Hikes");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.Segment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CoordinatesString")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("coordinates");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision")
                        .HasColumnName("distance");

                    b.Property<double>("ElevationGain")
                        .HasColumnType("double precision")
                        .HasColumnName("elevation_gain");

                    b.Property<double>("ElevationLoss")
                        .HasColumnType("double precision")
                        .HasColumnName("elevation_loss");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.SegmentHike", b =>
                {
                    b.Property<Guid>("SegmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("HikeId")
                        .HasColumnType("uuid");

                    b.HasKey("SegmentId", "HikeId");

                    b.HasIndex("HikeId");

                    b.ToTable("SegmentHike");
                });

            modelBuilder.Entity("HikingTracks.Domain.Photo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("content");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<Guid>("HikeID")
                        .HasColumnType("uuid")
                        .HasColumnName("hike_id");

                    b.Property<long>("Length")
                        .HasColumnType("bigint")
                        .HasColumnName("length");

                    b.HasKey("ID");

                    b.HasIndex("HikeID");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.Hike", b =>
                {
                    b.HasOne("HikingTracks.Domain.Entities.Account", "Account")
                        .WithMany("Hikes")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.SegmentHike", b =>
                {
                    b.HasOne("HikingTracks.Domain.Entities.Hike", "Hike")
                        .WithMany("SegmentsHikes")
                        .HasForeignKey("HikeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HikingTracks.Domain.Entities.Segment", "Segment")
                        .WithMany("SegmentsHikes")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hike");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("HikingTracks.Domain.Photo", b =>
                {
                    b.HasOne("HikingTracks.Domain.Entities.Hike", "Hike")
                        .WithMany("Photos")
                        .HasForeignKey("HikeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hike");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.Account", b =>
                {
                    b.Navigation("Hikes");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.Hike", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("SegmentsHikes");
                });

            modelBuilder.Entity("HikingTracks.Domain.Entities.Segment", b =>
                {
                    b.Navigation("SegmentsHikes");
                });
#pragma warning restore 612, 618
        }
    }
}
