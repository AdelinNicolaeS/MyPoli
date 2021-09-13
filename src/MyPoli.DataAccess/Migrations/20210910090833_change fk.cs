using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class changefk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_SubjectTeacher",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_IdSubject_IdTeacher",
                table: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_IdTeacher_IdSubject",
                table: "Grade",
                columns: new[] { "IdTeacher", "IdSubject" });

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_SubjectTeacher",
                table: "Grade",
                columns: new[] { "IdTeacher", "IdSubject" },
                principalTable: "SubjectTeacher",
                principalColumns: new[] { "TeacherId", "SubjectId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_SubjectTeacher",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_IdTeacher_IdSubject",
                table: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_IdSubject_IdTeacher",
                table: "Grade",
                columns: new[] { "IdSubject", "IdTeacher" });

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_SubjectTeacher",
                table: "Grade",
                columns: new[] { "IdSubject", "IdTeacher" },
                principalTable: "SubjectTeacher",
                principalColumns: new[] { "TeacherId", "SubjectId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
