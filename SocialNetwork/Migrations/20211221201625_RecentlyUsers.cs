using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class RecentlyUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccountId2",
                table: "Follower",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserAccountId2",
                table: "Follower",
                column: "UserAccountId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Follower_UserAccounts_UserAccountId2",
                table: "Follower",
                column: "UserAccountId2",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follower_UserAccounts_UserAccountId2",
                table: "Follower");

            migrationBuilder.DropIndex(
                name: "IX_Follower_UserAccountId2",
                table: "Follower");

            migrationBuilder.DropColumn(
                name: "UserAccountId2",
                table: "Follower");
        }
    }
}
