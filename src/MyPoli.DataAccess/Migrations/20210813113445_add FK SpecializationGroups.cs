using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class addFKSpecializationGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Gender",
            //    keyColumn: "Id",
            //    keyValue: new Guid("3a2f97ed-9088-4e22-a407-66954895c372"));

            //migrationBuilder.DeleteData(
            //    table: "Gender",
            //    keyColumn: "Id",
            //    keyValue: new Guid("3f69e8b8-e161-4e33-b94c-361e82f57a6b"));

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
            //    table: "Nationality",
            //    keyColumn: "Id",
            //    keyValue: new Guid("182b49e4-4513-4bb0-a2b2-260913151dac"));

            //migrationBuilder.DeleteData(
            //    table: "Nationality",
            //    keyColumn: "Id",
            //    keyValue: new Guid("55322079-bf4d-409e-99d0-c448a3f5f8ed"));

            //migrationBuilder.DeleteData(
            //    table: "PersonRoles",
            //    keyColumns: new[] { "PersonId", "RoleId" },
            //    keyValues: new object[] { new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"), 1 });

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: 3);

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

            //migrationBuilder.DeleteData(
            //    table: "Person",
            //    keyColumn: "Id",
            //    keyValue: new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"));

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "Gender",
            //    keyColumn: "Id",
            //    keyValue: new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"));

            //migrationBuilder.DeleteData(
            //    table: "Nationality",
            //    keyColumn: "Id",
            //    keyValue: new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"));

            migrationBuilder.CreateIndex(
                name: "IX_Group_SpecializationId",
                table: "Group",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecializationGroup",
                table: "Group",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializationGroup",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_SpecializationId",
                table: "Group");

            //migrationBuilder.InsertData(
            //    table: "Gender",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("3a2f97ed-9088-4e22-a407-66954895c372"), "male" },
            //        { new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"), "female" },
            //        { new Guid("3f69e8b8-e161-4e33-b94c-361e82f57a6b"), "other" }
            //    });

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
            //    table: "Nationality",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("182b49e4-4513-4bb0-a2b2-260913151dac"), "moldovean" },
            //        { new Guid("55322079-bf4d-409e-99d0-c448a3f5f8ed"), "tigan de matase" },
            //        { new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"), "roman" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { 1, "Secretary" },
            //        { 2, "Teacher" },
            //        { 3, "Student" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Status",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("f8de3c36-3c8f-4b4c-b97a-0b037b96d11e"), "inmatriculat" },
            //        { new Guid("e1b02dd5-0fcc-47e3-9221-614d46266c45"), "exmatriculat" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Subject",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("8b2214fd-63ef-4af4-a085-754f83904b5b"), "Utilizarea sistemelor de operare" },
            //        { new Guid("8982d7c8-a56a-4872-a3b7-bdda5a893aae"), "Matematica 1" },
            //        { new Guid("865b7084-f6a3-4aa3-8dfd-6d78239b93a9"), "Matematica 2" },
            //        { new Guid("03d7d3a1-d882-4c8a-91d0-be82e27ba3de"), "Programarea Calculatoarelor" },
            //        { new Guid("4063cb8a-14ab-4f28-aa6b-cddc1a8918a5"), "Metode Numerice" },
            //        { new Guid("019234af-b18b-4e0b-afee-60d865a66e87"), "Paradigme de Programare" },
            //        { new Guid("515bf6d3-c4d0-44a1-87f7-b41ae288165a"), "Analiza Algoritmilor" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Person",
            //    columns: new[] { "Id", "Address", "Birthday", "Email", "FirstName", "GenderId", "LastName", "NationalityId", "PasswordHash", "Phone", "RoleId" },
            //    values: new object[] { new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"), "Bdul Ala", new DateTime(1975, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "cristina@gribinic.ro", "Cristina", new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"), "Gribinic", new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"), "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4", "0712345678", 1 });

            //migrationBuilder.InsertData(
            //    table: "PersonRoles",
            //    columns: new[] { "PersonId", "RoleId" },
            //    values: new object[] { new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"), 1 });
        }
    }
}
