using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class addteacherandgrouptogrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdGroup",
                table: "Grade",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdTeacher",
                table: "Grade",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Grade_IdSubject_IdTeacher",
                table: "Grade",
                columns: new[] { "IdSubject", "IdTeacher" });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_IdTeacher_IdGroup",
                table: "Grade",
                columns: new[] { "IdTeacher", "IdGroup" });

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_SubjectTeacher",
                table: "Grade",
                columns: new[] { "IdSubject", "IdTeacher" },
                principalTable: "SubjectTeacher",
                principalColumns: new[] { "TeacherId", "SubjectId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_TeacherGroup",
                table: "Grade",
                columns: new[] { "IdTeacher", "IdGroup" },
                principalTable: "TeacherGroup",
                principalColumns: new[] { "IdTeacher", "IdGroup" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_SubjectTeacher",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_TeacherGroup",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_IdSubject_IdTeacher",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_IdTeacher_IdGroup",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "IdGroup",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "IdTeacher",
                table: "Grade");
        }
    }
}
