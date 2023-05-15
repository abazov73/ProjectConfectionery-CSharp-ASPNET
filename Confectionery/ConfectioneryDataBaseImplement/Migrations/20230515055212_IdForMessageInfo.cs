﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfectioneryDataBaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class IdForMessageInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MessageInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MessageInfos");
        }
    }
}
