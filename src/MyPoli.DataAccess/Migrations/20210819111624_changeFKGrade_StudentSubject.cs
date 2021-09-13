using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class changeFKGrade_StudentSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_StudentSubject_Grade",
                table: "StudentSubject");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_StudentSubject",
                table: "Grade",
                columns: new[] { "IdStudent", "IdSubject" },
                principalTable: "StudentSubject",
                principalColumns: new[] { "IdStudent", "IdSubject" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_StudentSubject",
                table: "Grade");

            migrationBuilder.AddForeignKey(
                name: "Fk_StudentSubject_Grade",
                table: "StudentSubject",
                columns: new[] { "IdStudent", "IdSubject" },
                principalTable: "Grade",
                principalColumns: new[] { "IdStudent", "IdSubject" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
