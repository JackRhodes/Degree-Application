using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Degree_Application.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_AccountId",
                table: "Items",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_AccountId",
                table: "Items",
                column: "AccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_AccountId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_AccountId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Items");
        }
    }
}
