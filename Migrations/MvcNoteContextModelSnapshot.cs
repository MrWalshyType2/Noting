﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Noting.Data;

namespace Noting.Migrations
{
    [DbContext(typeof(MvcNoteContext))]
    partial class MvcNoteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subtopic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("Noting.Models.NoteRelation", b =>
                {
                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChildId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ParentId");

                    b.HasIndex("ParentId1");

                    b.ToTable("NoteRelation");
                });

            modelBuilder.Entity("Noting.Models.NoteRelation", b =>
                {
                    b.HasOne("Noting.Models.Note", "Child")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Noting.Models.Note", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId1");

                    b.Navigation("Child");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Noting.Models.Note", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
