using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    public partial class DimensionsAndMaterialsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryID",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID",
                table: "Float");

            migrationBuilder.DropForeignKey(
                name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID1",
                table: "Float");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialRelationships_MaterialAndFinishes_MaterialAndFinishId",
                table: "ProductMaterialRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Restriction_PossibleDimensions_PossibleDimensionsID",
                table: "Restriction");

            migrationBuilder.DropTable(
                name: "MaterialAndFinishes");

            migrationBuilder.DropIndex(
                name: "IX_Restriction_PossibleDimensionsID",
                table: "Restriction");

            migrationBuilder.DropIndex(
                name: "IX_Float_DiscretePossibleDimensionsID",
                table: "Float");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "PossibleDimensionsID",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MaxHeightDimension",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "MaxWidthDimension",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "MinHeightDimension",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "MinWidthDimension",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "DiscretePossibleDimensionsID",
                table: "Float");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "DiscretePossibleDimensionsID1",
                table: "Float",
                newName: "DiscretePossibleValuesID");

            migrationBuilder.RenameIndex(
                name: "IX_Float_DiscretePossibleDimensionsID1",
                table: "Float",
                newName: "IX_Float_DiscretePossibleValuesID");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryID",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryID");

            migrationBuilder.AddColumn<float>(
                name: "MaxDepthValue",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxHeightValue",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxWidthValue",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinDepthValue",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinHeightValue",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinWidthValue",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepthPossibleValuesID",
                table: "PossibleDimensions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeightPossibleValuesID",
                table: "PossibleDimensions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WidthPossibleValuesID",
                table: "PossibleDimensions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PossibleValues",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    MinValue = table.Column<float>(nullable: true),
                    MaxValue = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleValues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Finishes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FinishName = table.Column<string>(nullable: true),
                    MaterialID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finishes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Finishes_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PossibleDimensions_DepthPossibleValuesID",
                table: "PossibleDimensions",
                column: "DepthPossibleValuesID");

            migrationBuilder.CreateIndex(
                name: "IX_PossibleDimensions_HeightPossibleValuesID",
                table: "PossibleDimensions",
                column: "HeightPossibleValuesID");

            migrationBuilder.CreateIndex(
                name: "IX_PossibleDimensions_WidthPossibleValuesID",
                table: "PossibleDimensions",
                column: "WidthPossibleValuesID");

            migrationBuilder.CreateIndex(
                name: "IX_Finishes_MaterialID",
                table: "Finishes",
                column: "MaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Float_PossibleValues_DiscretePossibleValuesID",
                table: "Float",
                column: "DiscretePossibleValuesID",
                principalTable: "PossibleValues",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleDimensions_PossibleValues_DepthPossibleValuesID",
                table: "PossibleDimensions",
                column: "DepthPossibleValuesID",
                principalTable: "PossibleValues",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleDimensions_PossibleValues_HeightPossibleValuesID",
                table: "PossibleDimensions",
                column: "HeightPossibleValuesID",
                principalTable: "PossibleValues",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleDimensions_PossibleValues_WidthPossibleValuesID",
                table: "PossibleDimensions",
                column: "WidthPossibleValuesID",
                principalTable: "PossibleValues",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialRelationships_Materials_MaterialAndFinishId",
                table: "ProductMaterialRelationships",
                column: "MaterialAndFinishId",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Float_PossibleValues_DiscretePossibleValuesID",
                table: "Float");

            migrationBuilder.DropForeignKey(
                name: "FK_PossibleDimensions_PossibleValues_DepthPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropForeignKey(
                name: "FK_PossibleDimensions_PossibleValues_HeightPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropForeignKey(
                name: "FK_PossibleDimensions_PossibleValues_WidthPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialRelationships_Materials_MaterialAndFinishId",
                table: "ProductMaterialRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Finishes");

            migrationBuilder.DropTable(
                name: "PossibleValues");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_PossibleDimensions_DepthPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropIndex(
                name: "IX_PossibleDimensions_HeightPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropIndex(
                name: "IX_PossibleDimensions_WidthPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MaxDepthValue",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MaxHeightValue",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MaxWidthValue",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MinDepthValue",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MinHeightValue",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MinWidthValue",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "DepthPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "HeightPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.DropColumn(
                name: "WidthPossibleValuesID",
                table: "PossibleDimensions");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "DiscretePossibleValuesID",
                table: "Float",
                newName: "DiscretePossibleDimensionsID1");

            migrationBuilder.RenameIndex(
                name: "IX_Float_DiscretePossibleValuesID",
                table: "Float",
                newName: "IX_Float_DiscretePossibleDimensionsID1");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Category",
                newName: "IX_Category_ParentCategoryID");

            migrationBuilder.AddColumn<int>(
                name: "PossibleDimensionsID",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxHeightDimension",
                table: "PossibleDimensions",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxWidthDimension",
                table: "PossibleDimensions",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinHeightDimension",
                table: "PossibleDimensions",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinWidthDimension",
                table: "PossibleDimensions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PossibleDimensions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DiscretePossibleDimensionsID",
                table: "Float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "MaterialAndFinishes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Finish = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialAndFinishes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_PossibleDimensionsID",
                table: "Restriction",
                column: "PossibleDimensionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Float_DiscretePossibleDimensionsID",
                table: "Float",
                column: "DiscretePossibleDimensionsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryID",
                table: "Category",
                column: "ParentCategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID",
                table: "Float",
                column: "DiscretePossibleDimensionsID",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID1",
                table: "Float",
                column: "DiscretePossibleDimensionsID1",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialRelationships_MaterialAndFinishes_MaterialAndFinishId",
                table: "ProductMaterialRelationships",
                column: "MaterialAndFinishId",
                principalTable: "MaterialAndFinishes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restriction_PossibleDimensions_PossibleDimensionsID",
                table: "Restriction",
                column: "PossibleDimensionsID",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
