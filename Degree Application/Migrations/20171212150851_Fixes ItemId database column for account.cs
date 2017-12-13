using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Degree_Application.Migrations
{
    public partial class FixesItemIddatabasecolumnforaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_AccountIdId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "AccountIdId",
                table: "Items",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_AccountIdId",
                table: "Items",
                newName: "IX_Items_AccountId");

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

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Items",
                newName: "AccountIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_AccountId",
                table: "Items",
                newName: "IX_Items_AccountIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_AccountIdId",
                table: "Items",
                column: "AccountIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
