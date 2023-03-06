using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFC03.EntityModels.ORM.Migrations
{
    public partial class FFCUpdate008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageList",
                table: "CommodityEvaluations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "Commodities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Producer",
                table: "Commodities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StorageMode",
                table: "Commodities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyTime",
                table: "Commodities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageList",
                table: "CommodityEvaluations");

            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "Commodities");

            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Commodities");

            migrationBuilder.DropColumn(
                name: "StorageMode",
                table: "Commodities");

            migrationBuilder.DropColumn(
                name: "WarrantyTime",
                table: "Commodities");
        }
    }
}
