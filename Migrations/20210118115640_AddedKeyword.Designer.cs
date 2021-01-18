﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Noting.Data;

namespace Noting.Migrations
{
    [DbContext(typeof(MvcNoteContext))]
    [Migration("20210118115640_AddedKeyword")]
    partial class AddedKeyword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Noting.Models.Keyword", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("NoteId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("Noting.Models.Note", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AutomaticIdLinking")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(20480)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subtopic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("Noting.Models.NoteRelation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChildId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.ToTable("NoteRelation");
                });

            modelBuilder.Entity("Noting.Models.SpacedRepetitionAttempt", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AttemptDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Correct")
                        .HasColumnType("bit");

                    b.Property<string>("SpacedRepetitionHistoryId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SpacedRepetitionHistoryId");

                    b.ToTable("SpacedRepetitionAttempts");
                });

            modelBuilder.Entity("Noting.Models.SpacedRepetitionHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextScheduledAttempt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NoteId")
                        .IsUnique();

                    b.ToTable("SpacedRepetitionHistories");
                });

            modelBuilder.Entity("Noting.Models.Keyword", b =>
                {
                    b.HasOne("Noting.Models.Note", null)
                        .WithMany("Keywords")
                        .HasForeignKey("NoteId");
                });

            modelBuilder.Entity("Noting.Models.NoteRelation", b =>
                {
                    b.HasOne("Noting.Models.Note", "Child")
                        .WithMany("Children")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Child");
                });

            modelBuilder.Entity("Noting.Models.SpacedRepetitionAttempt", b =>
                {
                    b.HasOne("Noting.Models.SpacedRepetitionHistory", "SpacedRepetitionHistory")
                        .WithMany("SpacedRepetitionAttempts")
                        .HasForeignKey("SpacedRepetitionHistoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SpacedRepetitionHistory");
                });

            modelBuilder.Entity("Noting.Models.SpacedRepetitionHistory", b =>
                {
                    b.HasOne("Noting.Models.Note", "Note")
                        .WithOne("SpacedRepetitionHistory")
                        .HasForeignKey("Noting.Models.SpacedRepetitionHistory", "NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Noting.Models.Note", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Keywords");

                    b.Navigation("SpacedRepetitionHistory");
                });

            modelBuilder.Entity("Noting.Models.SpacedRepetitionHistory", b =>
                {
                    b.Navigation("SpacedRepetitionAttempts");
                });
#pragma warning restore 612, 618
        }
    }
}
