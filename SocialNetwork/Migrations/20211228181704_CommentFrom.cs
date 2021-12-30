using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class CommentFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPostComments_UserIdentities_UserFromId",
                table: "UserPostComments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserPostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProfileImage",
                value: "/Uploads/admin.png");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostComments_Follower_UserFromId",
                table: "UserPostComments",
                column: "UserFromId",
                principalTable: "Follower",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPostComments_Follower_UserFromId",
                table: "UserPostComments");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "UserPostComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProfileImage",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostComments_UserIdentities_UserFromId",
                table: "UserPostComments",
                column: "UserFromId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
