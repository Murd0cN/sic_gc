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
    [Migration("20181004093028_NewCategory")]
    partial class NewCategory
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

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Category", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryID");
                });
#pragma warning restore 612, 618
        }
    }
}