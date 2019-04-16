using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoToDos.Web.Migrations
{
    public partial class ImportanceLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Importance",
                table: "Missions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importance",
                table: "Missions");
        }
    }
}
