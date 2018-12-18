using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    public partial class AdicionadaRelaçaoProdutoMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialAndFinishes_Products_ProductID",
                table: "MaterialAndFinishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PossibleDimensions_PossibleDimensionsID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_MaterialAndFinishes_ProductID",
                table: "MaterialAndFinishes");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "MaterialAndFinishes");

            migrationBuilder.AlterColumn<int>(
                name: "PossibleDimensionsID",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductMaterialRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    MaterialAndFinishId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterialRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaterialRelationships_MaterialAndFinishes_MaterialAndFinishId",
                        column: x => x.MaterialAndFinishId,
                        principalTable: "MaterialAndFinishes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductMaterialRelationships_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterialRelationships_MaterialAndFinishId",
                table: "ProductMaterialRelationships",
                column: "MaterialAndFinishId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterialRelationships_ProductId",
                table: "ProductMaterialRelationships",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PossibleDimensions_PossibleDimensionsID",
                table: "Products",
                column: "PossibleDimensionsID",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PossibleDimensions_PossibleDimensionsID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductMaterialRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "PossibleDimensionsID",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "MaterialAndFinishes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAndFinishes_ProductID",
                table: "MaterialAndFinishes",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialAndFinishes_Products_ProductID",
                table: "MaterialAndFinishes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PossibleDimensions_PossibleDimensionsID",
                table: "Products",
                column: "PossibleDimensionsID",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
