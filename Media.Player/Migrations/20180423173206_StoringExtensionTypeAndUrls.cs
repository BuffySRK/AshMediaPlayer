using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Media.Player.Migrations
{
    public partial class StoringExtensionTypeAndUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "MediaMetadata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaArtUrl",
                table: "MediaMetadata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaExtension",
                table: "MediaMetadata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "MediaMetadata",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "MediaMetadata");

            migrationBuilder.DropColumn(
                name: "MediaArtUrl",
                table: "MediaMetadata");

            migrationBuilder.DropColumn(
                name: "MediaExtension",
                table: "MediaMetadata");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "MediaMetadata");
        }
    }
}
