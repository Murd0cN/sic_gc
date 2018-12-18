﻿// <auto-generated />
using System;
using Arqsi_1160752_1161361_3DF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    [DbContext(typeof(ClosetContext))]
    [Migration("20181006181813_MaterialAndFinishAdded")]
    partial class MaterialAndFinishAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("ParentCategoryID");

                    b.HasKey("ID");

                    b.HasIndex("ParentCategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.MaterialAndFinish", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Finish");

                    b.Property<string>("Material");

                    b.HasKey("ID");

                    b.ToTable("MaterialAndFinishes");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Category", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Category", "ParentCategory")
                        .WithMany("ChildCategory")
                        .HasForeignKey("ParentCategoryID");
                });
#pragma warning restore 612, 618
        }
    }
}