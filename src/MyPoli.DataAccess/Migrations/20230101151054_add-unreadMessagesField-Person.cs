using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPoli.DataAccess.Migrations
{
    public partial class addunreadMessagesFieldPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnreadNotifications",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnreadNotifications",
                table: "Person");
        }
    }
}
