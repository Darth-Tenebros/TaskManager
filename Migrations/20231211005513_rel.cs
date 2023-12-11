using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    public partial class rel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeUserId",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeUserId",
                table: "Tasks",
                column: "AssigneeUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AssigneeUserId",
                table: "Tasks",
                column: "AssigneeUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AssigneeUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AssigneeUserId",
                table: "Tasks");
        }
    }
}
