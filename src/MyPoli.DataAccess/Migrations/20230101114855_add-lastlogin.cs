using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class addlastlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isSeenInGroup",
                table: "Student",
                newName: "IsSeenInGroup");

            migrationBuilder.RenameColumn(
                name: "isSeenInCourse",
                table: "Student",
                newName: "IsSeenInCourse");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Person",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "IsSeenInGroup",
                table: "Student",
                newName: "isSeenInGroup");

            migrationBuilder.RenameColumn(
                name: "IsSeenInCourse",
                table: "Student",
                newName: "isSeenInCourse");
        }
    }
}
