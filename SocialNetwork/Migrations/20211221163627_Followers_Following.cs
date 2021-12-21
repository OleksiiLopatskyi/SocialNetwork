using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class Followers_Following : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIdentities_UserAccounts_UserAccountId",
                table: "UserIdentities");

            migrationBuilder.DropIndex(
                name: "IX_UserIdentities_UserAccountId",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserIdentities");

            migrationBuilder.CreateTable(
                name: "Follower",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: true),
                    UserAccountId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follower_UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follower_UserAccounts_UserAccountId1",
                        column: x => x.UserAccountId1,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserAccountId",
                table: "Follower",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserAccountId1",
                table: "Follower",
                column: "UserAccountId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follower");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "UserIdentities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_UserAccountId",
                table: "UserIdentities",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIdentities_UserAccounts_UserAccountId",
                table: "UserIdentities",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
