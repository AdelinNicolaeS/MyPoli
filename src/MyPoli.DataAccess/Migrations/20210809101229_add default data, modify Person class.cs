using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class adddefaultdatamodifyPersonclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonRoles",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRoles", x => new { x.PersonId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PersonRoles_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3a2f97ed-9088-4e22-a407-66954895c372"), "male" },
                    { new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"), "female" },
                    { new Guid("3f69e8b8-e161-4e33-b94c-361e82f57a6b"), "other" }
                });

            migrationBuilder.InsertData(
                table: "Nationality",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"), "roman" },
                    { new Guid("182b49e4-4513-4bb0-a2b2-260913151dac"), "moldovean" },
                    { new Guid("55322079-bf4d-409e-99d0-c448a3f5f8ed"), "tigan de matase" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Secretary");

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Address", "Birthday", "Email", "FirstName", "GenderId", "LastName", "NationalityId", "PasswordHash", "Phone", "RoleId" },
                values: new object[] { new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"), "Bdul Ala", new DateTime(1975, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "cristina@gribinic.ro", "Cristina", new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"), "Gribinic", new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"), "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4", "0712345678", 1 });

            migrationBuilder.InsertData(
                table: "PersonRoles",
                columns: new[] { "PersonId", "RoleId" },
                values: new object[] { new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PersonRoles_RoleId",
                table: "PersonRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRoles");

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: new Guid("3a2f97ed-9088-4e22-a407-66954895c372"));

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: new Guid("3f69e8b8-e161-4e33-b94c-361e82f57a6b"));

            migrationBuilder.DeleteData(
                table: "Nationality",
                keyColumn: "Id",
                keyValue: new Guid("182b49e4-4513-4bb0-a2b2-260913151dac"));

            migrationBuilder.DeleteData(
                table: "Nationality",
                keyColumn: "Id",
                keyValue: new Guid("55322079-bf4d-409e-99d0-c448a3f5f8ed"));

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"));

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"));

            migrationBuilder.DeleteData(
                table: "Nationality",
                keyColumn: "Id",
                keyValue: new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"));

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GenderId = table.Column<byte>(type: "tinyint", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PrivacyId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "GenderId", "LastName", "PasswordHash", "PrivacyId" },
                values: new object[] { new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"), "adelin.stanca@essensys.ro", "Stanca", (byte)1, "Adelin", "123456", (byte)1 });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac") });

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_UserId",
                table: "UsersRoles",
                column: "UserId");
        }
    }
}
