using Microsoft.EntityFrameworkCore.Migrations;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    public partial class ChangeDimension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restriction_PossibleDimensions_DimensionsID",
                table: "Restriction");

            migrationBuilder.DropIndex(
                name: "IX_Restriction_DimensionsID",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "DimensionsID",
                table: "Restriction");

            migrationBuilder.RenameColumn(
                name: "MinDimension",
                table: "PossibleDimensions",
                newName: "MinWidthDimension");

            migrationBuilder.RenameColumn(
                name: "MaxDimension",
                table: "PossibleDimensions",
                newName: "MinHeightDimension");

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

            migrationBuilder.AddColumn<int>(
                name: "DiscretePossibleDimensionsID1",
                table: "Float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_PossibleDimensionsID",
                table: "Restriction",
                column: "PossibleDimensionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Float_DiscretePossibleDimensionsID1",
                table: "Float",
                column: "DiscretePossibleDimensionsID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID1",
                table: "Float",
                column: "DiscretePossibleDimensionsID1",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restriction_PossibleDimensions_PossibleDimensionsID",
                table: "Restriction",
                column: "PossibleDimensionsID",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Float_PossibleDimensions_DiscretePossibleDimensionsID1",
                table: "Float");

            migrationBuilder.DropForeignKey(
                name: "FK_Restriction_PossibleDimensions_PossibleDimensionsID",
                table: "Restriction");

            migrationBuilder.DropIndex(
                name: "IX_Restriction_PossibleDimensionsID",
                table: "Restriction");

            migrationBuilder.DropIndex(
                name: "IX_Float_DiscretePossibleDimensionsID1",
                table: "Float");

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
                name: "DiscretePossibleDimensionsID1",
                table: "Float");

            migrationBuilder.RenameColumn(
                name: "MinWidthDimension",
                table: "PossibleDimensions",
                newName: "MinDimension");

            migrationBuilder.RenameColumn(
                name: "MinHeightDimension",
                table: "PossibleDimensions",
                newName: "MaxDimension");

            migrationBuilder.AddColumn<int>(
                name: "DimensionsID",
                table: "Restriction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_DimensionsID",
                table: "Restriction",
                column: "DimensionsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restriction_PossibleDimensions_DimensionsID",
                table: "Restriction",
                column: "DimensionsID",
                principalTable: "PossibleDimensions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
