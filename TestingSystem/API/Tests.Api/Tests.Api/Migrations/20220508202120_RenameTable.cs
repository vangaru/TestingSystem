using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tests.Api.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectableQuestionNames");

            migrationBuilder.CreateTable(
                name: "SelectableAnswer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectableAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectableAnswer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectableAnswer_QuestionId",
                table: "SelectableAnswer",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectableAnswer");

            migrationBuilder.CreateTable(
                name: "SelectableQuestionNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
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
    }
}
