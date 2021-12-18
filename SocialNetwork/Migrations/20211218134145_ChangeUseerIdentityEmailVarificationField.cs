using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class ChangeUseerIdentityEmailVarificationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailVerificationCode",
                table: "UserIdentities",
                newName: "VerificationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerificationCode",
                table: "UserIdentities",
                newName: "EmailVerificationCode");
        }
    }
}
