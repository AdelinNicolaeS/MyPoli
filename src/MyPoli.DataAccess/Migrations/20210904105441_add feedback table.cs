using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class addfeedbacktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSubject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStudent = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectureOpinion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureGrade = table.Column<int>(type: "int", nullable: false),
                    SeminarOpinion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeminarGrade = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_StudentSubject",
                        columns: x => new { x.IdStudent, x.IdSubject },
                        principalTable: "StudentSubject",
                        principalColumns: new[] { "IdStudent", "IdSubject" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_IdStudent_IdSubject",
                table: "Feedback",
                columns: new[] { "IdStudent", "IdSubject" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");
        }
    }
}
