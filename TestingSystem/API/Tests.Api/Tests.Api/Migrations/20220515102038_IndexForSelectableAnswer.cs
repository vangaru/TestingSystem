﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tests.Api.Migrations
{
    public partial class IndexForSelectableAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "SelectableAnswer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "SelectableAnswer");
        }
    }
}
