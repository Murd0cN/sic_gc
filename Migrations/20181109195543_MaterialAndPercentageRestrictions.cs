using Microsoft.EntityFrameworkCore.Migrations;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    public partial class MaterialAndPercentageRestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialRelationships_Materials_MaterialAndFinishId",
                table: "ProductMaterialRelationships");

            migrationBuilder.RenameColumn(
                name: "MaterialAndFinishId",
                table: "ProductMaterialRelationships",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialRelationships_MaterialAndFinishId",
                table: "ProductMaterialRelationships",
                newName: "IX_ProductMaterialRelationships_MaterialId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMandatory",
                table: "ProductRelationships",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialRelationships_Materials_MaterialId",
                table: "ProductMaterialRelationships",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialRelationships_Materials_MaterialId",
                table: "ProductMaterialRelationships");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                table: "ProductRelationships");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "ProductMaterialRelationships",
                newName: "MaterialAndFinishId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialRelationships_MaterialId",
                table: "ProductMaterialRelationships",
                newName: "IX_ProductMaterialRelationships_MaterialAndFinishId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialRelationships_Materials_MaterialAndFinishId",
                table: "ProductMaterialRelationships",
                column: "MaterialAndFinishId",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
