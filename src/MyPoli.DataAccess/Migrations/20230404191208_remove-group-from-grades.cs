using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class removegroupfromgrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_TeacherGroup",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_IdTeacher_IdGroup",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "IdGroup",
                table: "Grade");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "NotificationType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "NotificationType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "IdGroup",
                table: "Grade",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Grade_IdTeacher_IdGroup",
                table: "Grade",
                columns: new[] { "IdTeacher", "IdGroup" });

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_TeacherGroup",
                table: "Grade",
                columns: new[] { "IdTeacher", "IdGroup" },
                principalTable: "TeacherGroup",
                principalColumns: new[] { "IdTeacher", "IdGroup" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
