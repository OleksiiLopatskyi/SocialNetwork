using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class EmailStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "isEmailConfirmed",
                table: "UserIdentities",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "UserIdentities",
                keyColumn: "Id",
                keyValue: 1,
                column: "isEmailConfirmed",
                value: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isEmailConfirmed",
                table: "UserIdentities",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "UserIdentities",
                keyColumn: "Id",
                keyValue: 1,
                column: "isEmailConfirmed",
                value: false);
        }
    }
}
