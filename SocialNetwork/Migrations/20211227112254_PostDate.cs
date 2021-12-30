using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class PostDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPosts_UserAccounts_UserAccountId",
                table: "UserPosts");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccountId",
                table: "UserPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "UserPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_UserAccounts_UserAccountId",
                table: "UserPosts",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPosts_UserAccounts_UserAccountId",
                table: "UserPosts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserPosts");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccountId",
                table: "UserPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_UserAccounts_UserAccountId",
                table: "UserPosts",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
