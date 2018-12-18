using Microsoft.EntityFrameworkCore.Migrations;

namespace Arqsi_1160752_1161361_3DF.Migrations
{
    public partial class DbSetNovasRestricoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MaxDepthPercentage",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxHeightPercentage",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxWidthPercentage",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinDepthPercentage",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinHeightPercentage",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinWidthPercentage",
                table: "Restriction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDepthPercentage",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MaxHeightPercentage",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MaxWidthPercentage",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MinDepthPercentage",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MinHeightPercentage",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "MinWidthPercentage",
                table: "Restriction");
        }
    }
}
