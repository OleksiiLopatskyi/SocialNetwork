using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeId = table.Column<int>(type: "int", nullable: true),
                    WithId = table.Column<int>(type: "int", nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_UserChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserIdentities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    PostCommentId = table.Column<int>(type: "int", nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: true),
                    UserPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIdentities_UserMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "UserMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdentityId = table.Column<int>(type: "int", nullable: true),
                    UserInfoId = table.Column<int>(type: "int", nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccounts_UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccounts_UserIdentities_UserIdentityId",
                        column: x => x.UserIdentityId,
                        principalTable: "UserIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccounts_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPosts_UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPostComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFromId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPostComments_UserIdentities_UserFromId",
                        column: x => x.UserFromId,
                        principalTable: "UserIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPostComments_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserAccountId",
                table: "UserAccounts",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserIdentityId",
                table: "UserAccounts",
                column: "UserIdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserInfoId",
                table: "UserAccounts",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_MeId",
                table: "UserChats",
                column: "MeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_UserAccountId",
                table: "UserChats",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_WithId",
                table: "UserChats",
                column: "WithId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_MessageId",
                table: "UserIdentities",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_PostCommentId",
                table: "UserIdentities",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_UserAccountId",
                table: "UserIdentities",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_UserPostId",
                table: "UserIdentities",
                column: "UserPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ChatId",
                table: "UserMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPostComments_UserFromId",
                table: "UserPostComments",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPostComments_UserPostId",
                table: "UserPostComments",
                column: "UserPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPosts_UserAccountId",
                table: "UserPosts",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_UserAccounts_UserAccountId",
                table: "UserChats",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_UserIdentities_MeId",
                table: "UserChats",
                column: "MeId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_UserIdentities_WithId",
                table: "UserChats",
                column: "WithId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIdentities_UserAccounts_UserAccountId",
                table: "UserIdentities",
                column: "UserAccountId",
                principalTable: "UserAccounts",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserIdentities_UserIdentityId",
                table: "UserAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_UserIdentities_MeId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_UserIdentities_WithId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostComments_UserIdentities_UserFromId",
                table: "UserPostComments");

            migrationBuilder.DropTable(
                name: "UserIdentities");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "UserPostComments");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropTable(
                name: "UserPosts");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
