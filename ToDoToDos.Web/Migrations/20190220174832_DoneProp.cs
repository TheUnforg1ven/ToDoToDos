using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoToDos.Web.Migrations
{
    public partial class DoneProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Missions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Missions");
        }
    }
}
