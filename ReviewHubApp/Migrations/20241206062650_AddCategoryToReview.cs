﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewHubApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");
        }
    }
}
