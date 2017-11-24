using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Degree_Application.Migrations
{
    public partial class Accountmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AccountModel",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AccountModel",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AccountModel",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "AccountModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AccountModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AccountModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AccountModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "AccountModel",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AccountModel",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "AccountModel");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "AccountModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AccountModel",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AccountModel",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AccountModel",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
