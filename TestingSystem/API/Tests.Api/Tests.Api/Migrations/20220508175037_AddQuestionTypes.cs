using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tests.Api.Migrations
{
    public partial class AddQuestionTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionType",
                table: "Questions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SelectableQuestionNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectableQuestionNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectableQuestionNames_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectableQuestionNames_QuestionId",
                table: "SelectableQuestionNames",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectableQuestionNames");

            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "Questions");
        }
    }
}
