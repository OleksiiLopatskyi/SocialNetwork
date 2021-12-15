using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class Changecommentclass_addlikeclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIdentities_UserMessages_MessageId",
                table: "UserIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserIdentities_UserPostComments_PostCommentId",
                table: "UserIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserIdentities_UserPosts_UserPostId",
                table: "UserIdentities");

            migrationBuilder.DropIndex(
                name: "IX_UserIdentities_MessageId",
                table: "UserIdentities");

            migrationBuilder.DropIndex(
                name: "IX_UserIdentities_PostCommentId",
                table: "UserIdentities");

            migrationBuilder.DropIndex(
                name: "IX_UserIdentities_UserPostId",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "PostCommentId",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "UserPostId",
                table: "UserIdentities");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    UserPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_UserMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "UserMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_UserPostComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "UserPostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_MessageId",
                table: "Likes",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserPostId",
                table: "Likes",
                column: "UserPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "UserIdentities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostCommentId",
                table: "UserIdentities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserPostId",
                table: "UserIdentities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_MessageId",
                table: "UserIdentities",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_PostCommentId",
                table: "UserIdentities",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_UserPostId",
                table: "UserIdentities",
                column: "UserPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIdentities_UserMessages_MessageId",
                table: "UserIdentities",
                column: "MessageId",
                principalTable: "UserMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIdentities_UserPostComments_PostCommentId",
                table: "UserIdentities",
                column: "PostCommentId",
                principalTable: "UserPostComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIdentities_UserPosts_UserPostId",
                table: "UserIdentities",
                column: "UserPostId",
                principalTable: "UserPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
