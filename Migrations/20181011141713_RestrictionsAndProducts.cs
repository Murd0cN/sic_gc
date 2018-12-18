using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    public partial class RestrictionsAndProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "MaterialAndFinishes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PossibleDimensions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    MinDimension = table.Column<float>(nullable: true),
                    MaxDimension = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleDimensions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Float",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FloatValue = table.Column<float>(nullable: false),
                    DiscretePossibleDimensionsID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Float", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID",
                        column: x => x.DiscretePossibleDimensionsID,
                        principalTable: "PossibleDimensions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProductCategoryID = table.Column<int>(nullable: false),
                    PossibleDimensionsID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_PossibleDimensions_PossibleDimensionsID",
                        column: x => x.PossibleDimensionsID,
                        principalTable: "PossibleDimensions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Category_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductRelationships",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentProductID = table.Column<int>(nullable: false),
                    ChildProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelationships", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductRelationships_Products_ChildProductID",
                        column: x => x.ChildProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRelationships_Products_ParentProductID",
                        column: x => x.ParentProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Restriction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    ProductRelationshipID = table.Column<int>(nullable: true),
                    DimensionsID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Restriction_PossibleDimensions_DimensionsID",
                        column: x => x.DimensionsID,
                        principalTable: "PossibleDimensions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Restriction_ProductRelationships_ProductRelationshipID",
                        column: x => x.ProductRelationshipID,
                        principalTable: "ProductRelationships",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAndFinishes_ProductID",
                table: "MaterialAndFinishes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Float_DiscretePossibleDimensionsID",
                table: "Float",
                column: "DiscretePossibleDimensionsID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelationships_ChildProductID",
                table: "ProductRelationships",
                column: "ChildProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelationships_ParentProductID",
                table: "ProductRelationships",
                column: "ParentProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PossibleDimensionsID",
                table: "Products",
                column: "PossibleDimensionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_DimensionsID",
                table: "Restriction",
                column: "DimensionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_ProductRelationshipID",
                table: "Restriction",
                column: "ProductRelationshipID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialAndFinishes_Products_ProductID",
                table: "MaterialAndFinishes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialAndFinishes_Products_ProductID",
                table: "MaterialAndFinishes");

            migrationBuilder.DropTable(
                name: "Float");

            migrationBuilder.DropTable(
                name: "Restriction");

            migrationBuilder.DropTable(
                name: "ProductRelationships");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PossibleDimensions");

            migrationBuilder.DropIndex(
                name: "IX_MaterialAndFinishes_ProductID",
                table: "MaterialAndFinishes");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "MaterialAndFinishes");
        }
    }
}
