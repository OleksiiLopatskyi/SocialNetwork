using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class UserIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserAccounts_UserAccountId",
                table: "UserAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserIdentities_UserIdentityId",
                table: "UserAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserInfos_UserInfoId",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_UserAccountId",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserAccounts");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "UserInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserIdentities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserIdentityId",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "Age", "City", "Country", "ProfileImage", "Status", "UserAccountId" },
                values: new object[] { 1, 18, "Lviv", "Ukraine", null, 0, null });

            migrationBuilder.InsertData(
                table: "UserIdentities",
                columns: new[] { "Id", "Email", "Password", "RoleId", "UserAccountId", "Username" },
                values: new object[] { 1, "admin@gmail.com", "12345", 1, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "UserIdentityId", "UserInfoId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserAccountId",
                table: "UserInfos",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_RoleId",
                table: "UserIdentities",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserIdentities_UserIdentityId",
                table: "UserAccounts",
                column: "UserIdentityId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserInfos_UserInfoId",
                table: "UserAccounts",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIdentities_Roles_RoleId",
                table: "UserIdentities",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_UserAccounts_UserAccountId",
                table: "UserInfos",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserIdentities_UserIdentityId",
                table: "UserAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserInfos_UserInfoId",
                table: "UserAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserIdentities_Roles_RoleId",
                table: "UserIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_UserAccounts_UserAccountId",
                table: "UserInfos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserAccountId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserIdentities_RoleId",
                table: "UserIdentities");

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserIdentities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserIdentities");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "UserAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserIdentityId",
                table: "UserAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "UserAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserAccountId",
                table: "UserAccounts",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserAccounts_UserAccountId",
                table: "UserAccounts",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserIdentities_UserIdentityId",
                table: "UserAccounts",
                column: "UserIdentityId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserInfos_UserInfoId",
                table: "UserAccounts",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
