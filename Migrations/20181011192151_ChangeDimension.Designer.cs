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
    [Migration("20181011192151_ChangeDimension")]
    partial class ChangeDimension
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

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Float", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DiscretePossibleDimensionsID");

                    b.Property<int?>("DiscretePossibleDimensionsID1");

                    b.Property<float>("FloatValue");

                    b.HasKey("ID");

                    b.HasIndex("DiscretePossibleDimensionsID");

                    b.HasIndex("DiscretePossibleDimensionsID1");

                    b.ToTable("Float");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.MaterialAndFinish", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Finish");

                    b.Property<string>("Material");

                    b.Property<int?>("ProductID");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("MaterialAndFinishes");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.PossibleDimensions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("PossibleDimensions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PossibleDimensions");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("PossibleDimensionsID");

                    b.Property<int>("ProductCategoryID");

                    b.HasKey("ID");

                    b.HasIndex("PossibleDimensionsID");

                    b.HasIndex("ProductCategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.ProductRelationship", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChildProductID");

                    b.Property<int>("ParentProductID");

                    b.HasKey("ID");

                    b.HasIndex("ChildProductID");

                    b.HasIndex("ParentProductID");

                    b.ToTable("ProductRelationships");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Restriction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int?>("ProductRelationshipID");

                    b.HasKey("ID");

                    b.HasIndex("ProductRelationshipID");

                    b.ToTable("Restriction");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Restriction");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.ContinuousPossibleDimensions", b =>
                {
                    b.HasBaseType("Arqsi_1160752_1161361_3DF.Models.PossibleDimensions");

                    b.Property<float>("MaxHeightDimension");

                    b.Property<float>("MaxWidthDimension");

                    b.Property<float>("MinHeightDimension");

                    b.Property<float>("MinWidthDimension");

                    b.ToTable("ContinuousPossibleDimensions");

                    b.HasDiscriminator().HasValue("ContinuousPossibleDimensions");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.DiscretePossibleDimensions", b =>
                {
                    b.HasBaseType("Arqsi_1160752_1161361_3DF.Models.PossibleDimensions");


                    b.ToTable("DiscretePossibleDimensions");

                    b.HasDiscriminator().HasValue("DiscretePossibleDimensions");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.DimensionsRestriction", b =>
                {
                    b.HasBaseType("Arqsi_1160752_1161361_3DF.Models.Restriction");

                    b.Property<int>("PossibleDimensionsID");

                    b.HasIndex("PossibleDimensionsID");

                    b.ToTable("DimensionsRestriction");

                    b.HasDiscriminator().HasValue("DimensionsRestriction");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Category", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Category", "ParentCategory")
                        .WithMany("ChildCategory")
                        .HasForeignKey("ParentCategoryID");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Float", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.DiscretePossibleDimensions")
                        .WithMany("HeightPossibleDims")
                        .HasForeignKey("DiscretePossibleDimensionsID");

                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.DiscretePossibleDimensions")
                        .WithMany("WidthPossibleDims")
                        .HasForeignKey("DiscretePossibleDimensionsID1");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.MaterialAndFinish", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Product")
                        .WithMany("Materials")
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Product", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.PossibleDimensions", "PossibleDimensions")
                        .WithMany()
                        .HasForeignKey("PossibleDimensionsID");

                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Category", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.ProductRelationship", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Product", "ChildProduct")
                        .WithMany()
                        .HasForeignKey("ChildProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.Product", "ParentProduct")
                        .WithMany()
                        .HasForeignKey("ParentProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.Restriction", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.ProductRelationship")
                        .WithMany("Restrictions")
                        .HasForeignKey("ProductRelationshipID");
                });

            modelBuilder.Entity("Arqsi_1160752_1161361_3DF.Models.DimensionsRestriction", b =>
                {
                    b.HasOne("Arqsi_1160752_1161361_3DF.Models.PossibleDimensions", "Dimensions")
                        .WithMany()
                        .HasForeignKey("PossibleDimensionsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
