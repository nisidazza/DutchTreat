using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DutchTreat.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderDate", "OrderNumber" },
                values: new object[] {
                    new DateTime(2020, 4, 17, 3, 48, 55, 382, DateTimeKind.Utc).AddTicks(3684),
                    "12345"
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}