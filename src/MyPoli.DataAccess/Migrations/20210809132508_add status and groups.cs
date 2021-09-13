using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class addstatusandgroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "Name", "SpecializationId" },
                values: new object[,]
                {
                    { new Guid("210a512b-18e0-416e-ac72-952bb9b0731c"), "321CA", null },
                    { new Guid("e9c870e7-c7b9-4638-b965-d349f8002ea7"), "321CB", null },
                    { new Guid("79987d7d-b67f-4968-8aec-966376fa6ff5"), "321CC", null },
                    { new Guid("8acc70ce-ef0e-4ec0-b707-fa8257eb9731"), "331CA", null },
                    { new Guid("a04c2ee7-2ae4-41a9-b5ff-de5c5e97d0ce"), "321CD", null },
                    { new Guid("18cae655-d068-4089-8e52-9d5bf3fb4134"), "332CA", null }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5c71f99e-67a0-4ae0-a428-2ac74e9c5393"), "inmatriculat" },
                    { new Guid("c8ba7a83-b8d5-4638-82d9-3a83b90e00fa"), "exmatriculat" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("18cae655-d068-4089-8e52-9d5bf3fb4134"));

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("210a512b-18e0-416e-ac72-952bb9b0731c"));

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("79987d7d-b67f-4968-8aec-966376fa6ff5"));

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("8acc70ce-ef0e-4ec0-b707-fa8257eb9731"));

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("a04c2ee7-2ae4-41a9-b5ff-de5c5e97d0ce"));

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("e9c870e7-c7b9-4638-b965-d349f8002ea7"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: new Guid("5c71f99e-67a0-4ae0-a428-2ac74e9c5393"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: new Guid("c8ba7a83-b8d5-4638-82d9-3a83b90e00fa"));
        }
    }
}
