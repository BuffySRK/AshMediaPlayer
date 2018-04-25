using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Media.Player.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaMetadata",
                columns: table => new
                {
                    MediaMetadataId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(maxLength: 500, nullable: false),
                    Information = table.Column<string>(nullable: true),
                    MediaArtUrl = table.Column<string>(nullable: true),
                    MediaExtension = table.Column<string>(nullable: false),
                    MediaUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaMetadata", x => x.MediaMetadataId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaMetadata");
        }
    }
}
