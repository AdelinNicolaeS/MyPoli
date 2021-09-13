using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class adddefaultsubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("18cae655-d068-4089-8e52-9d5bf3fb4134"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("210a512b-18e0-416e-ac72-952bb9b0731c"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("79987d7d-b67f-4968-8aec-966376fa6ff5"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("8acc70ce-ef0e-4ec0-b707-fa8257eb9731"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("a04c2ee7-2ae4-41a9-b5ff-de5c5e97d0ce"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("e9c870e7-c7b9-4638-b965-d349f8002ea7"));

            //migrationBuilder.DeleteData(
            //    table: "Status",
            //    keyColumn: "Id",
            //    keyValue: new Guid("5c71f99e-67a0-4ae0-a428-2ac74e9c5393"));

            //migrationBuilder.DeleteData(
            //    table: "Status",
            //    keyColumn: "Id",
            //    keyValue: new Guid("c8ba7a83-b8d5-4638-82d9-3a83b90e00fa"));

            //migrationBuilder.InsertData(
            //    table: "Group",
            //    columns: new[] { "Id", "Name", "SpecializationId" },
            //    values: new object[,]
            //    {
            //        { new Guid("5b144eaf-fef8-4e09-b7ff-b898efb233ba"), "321CA", null },
            //        { new Guid("08342fcd-8c23-46ef-a3f1-3113fe3d8980"), "321CB", null },
            //        { new Guid("bc159116-cc98-457e-b1ea-9e74fb81824b"), "321CC", null },
            //        { new Guid("7e82d29b-2144-4cc9-9ef4-bb37e6e41aba"), "331CA", null },
            //        { new Guid("a6750a62-531d-48c6-ac74-ad7a981f9a45"), "321CD", null },
            //        { new Guid("56a6c5ba-8721-4c12-b323-fe5a210e7720"), "332CA", null }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Status",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("f8de3c36-3c8f-4b4c-b97a-0b037b96d11e"), "inmatriculat" },
            //        { new Guid("e1b02dd5-0fcc-47e3-9221-614d46266c45"), "exmatriculat" }
            //    });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8982d7c8-a56a-4872-a3b7-bdda5a893aae"), "Matematica 1" },
                    { new Guid("865b7084-f6a3-4aa3-8dfd-6d78239b93a9"), "Matematica 2" },
                    { new Guid("03d7d3a1-d882-4c8a-91d0-be82e27ba3de"), "Programarea Calculatoarelor" },
                    { new Guid("4063cb8a-14ab-4f28-aa6b-cddc1a8918a5"), "Metode Numerice" },
                    { new Guid("019234af-b18b-4e0b-afee-60d865a66e87"), "Paradigme de Programare" },
                    { new Guid("8b2214fd-63ef-4af4-a085-754f83904b5b"), "Utilizarea sistemelor de operare" },
                    { new Guid("515bf6d3-c4d0-44a1-87f7-b41ae288165a"), "Analiza Algoritmilor" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("08342fcd-8c23-46ef-a3f1-3113fe3d8980"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("56a6c5ba-8721-4c12-b323-fe5a210e7720"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("5b144eaf-fef8-4e09-b7ff-b898efb233ba"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("7e82d29b-2144-4cc9-9ef4-bb37e6e41aba"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("a6750a62-531d-48c6-ac74-ad7a981f9a45"));

            //migrationBuilder.DeleteData(
            //    table: "Group",
            //    keyColumn: "Id",
            //    keyValue: new Guid("bc159116-cc98-457e-b1ea-9e74fb81824b"));

            //migrationBuilder.DeleteData(
            //    table: "Status",
            //    keyColumn: "Id",
            //    keyValue: new Guid("e1b02dd5-0fcc-47e3-9221-614d46266c45"));

            //migrationBuilder.DeleteData(
            //    table: "Status",
            //    keyColumn: "Id",
            //    keyValue: new Guid("f8de3c36-3c8f-4b4c-b97a-0b037b96d11e"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("019234af-b18b-4e0b-afee-60d865a66e87"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("03d7d3a1-d882-4c8a-91d0-be82e27ba3de"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("4063cb8a-14ab-4f28-aa6b-cddc1a8918a5"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("515bf6d3-c4d0-44a1-87f7-b41ae288165a"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("865b7084-f6a3-4aa3-8dfd-6d78239b93a9"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("8982d7c8-a56a-4872-a3b7-bdda5a893aae"));

            //migrationBuilder.DeleteData(
            //    table: "Subject",
            //    keyColumn: "Id",
            //    keyValue: new Guid("8b2214fd-63ef-4af4-a085-754f83904b5b"));

            //migrationBuilder.InsertData(
            //    table: "Group",
            //    columns: new[] { "Id", "Name", "SpecializationId" },
            //    values: new object[,]
            //    {
            //        { new Guid("210a512b-18e0-416e-ac72-952bb9b0731c"), "321CA", null },
            //        { new Guid("e9c870e7-c7b9-4638-b965-d349f8002ea7"), "321CB", null },
            //        { new Guid("79987d7d-b67f-4968-8aec-966376fa6ff5"), "321CC", null },
            //        { new Guid("8acc70ce-ef0e-4ec0-b707-fa8257eb9731"), "331CA", null },
            //        { new Guid("a04c2ee7-2ae4-41a9-b5ff-de5c5e97d0ce"), "321CD", null },
            //        { new Guid("18cae655-d068-4089-8e52-9d5bf3fb4134"), "332CA", null }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Status",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("5c71f99e-67a0-4ae0-a428-2ac74e9c5393"), "inmatriculat" },
            //        { new Guid("c8ba7a83-b8d5-4638-82d9-3a83b90e00fa"), "exmatriculat" }
            //    });
        }
    }
}
