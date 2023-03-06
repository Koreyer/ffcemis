using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFC03.EntityModels.ORM.Migrations
{
    public partial class FFCUpdate009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WarrantyTime",
                table: "Commodities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WarrantyTime",
                table: "Commodities",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
