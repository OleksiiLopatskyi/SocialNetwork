using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class PostImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserPosts");

            migrationBuilder.CreateTable(
                name: "ImagePost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagePost_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagePost_UserPostId",
                table: "ImagePost",
                column: "UserPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagePost");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "UserPosts",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
