using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tests.Api.Migrations
{
    public partial class AddQuestionNamename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SelectableQuestionNames",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SelectableQuestionNames");
        }
    }
}
