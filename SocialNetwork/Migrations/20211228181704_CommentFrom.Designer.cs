﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialNetwork.Models.Database;

namespace SocialNetwork.Migrations
{
    [DbContext(typeof(SocialNetworkContext))]
    [Migration("20211228181704_CommentFrom")]
    partial class CommentFrom
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialNetwork.Models.ChatModels.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MeId")
                        .HasColumnType("int");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("WithId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeId");

                    b.HasIndex("UserAccountId");

                    b.HasIndex("WithId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("SocialNetwork.Models.ChatModels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("UserMessages");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserFromId")
                        .HasColumnType("int");

                    b.Property<int?>("UserPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserPostId");

                    b.ToTable("UserPostComments");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Follower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("UserAccountId1")
                        .HasColumnType("int");

                    b.Property<int?>("UserAccountId2")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.HasIndex("UserAccountId1");

                    b.HasIndex("UserAccountId2");

                    b.ToTable("Follower");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.ImagePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserPostId");

                    b.ToTable("ImagePost");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MessageId")
                        .HasColumnType("int");

                    b.Property<int?>("UserPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserPostId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<int>("UserIdentityId")
                        .HasColumnType("int");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserIdentityId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("UserAccounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserAccountId = 0,
                            UserIdentityId = 1,
                            UserInfoId = 1
                        });
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResetPasswordtStatus")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("isEmailConfirmed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UserIdentities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            Password = "12345",
                            ResetPasswordtStatus = 0,
                            RoleId = 2,
                            Username = "admin",
                            isEmailConfirmed = 0
                        });
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.ToTable("UserInfos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 18,
                            City = "Lviv",
                            Country = "Ukraine",
                            ProfileImage = "/Uploads/admin.png",
                            Status = 0
                        });
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.ToTable("UserPosts");
                });

            modelBuilder.Entity("SocialNetwork.Models.ChatModels.Chat", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.UserIdentity", "Me")
                        .WithMany()
                        .HasForeignKey("MeId");

                    b.HasOne("SocialNetwork.Models.UserModels.UserAccount", null)
                        .WithMany("UserChats")
                        .HasForeignKey("UserAccountId");

                    b.HasOne("SocialNetwork.Models.UserModels.UserIdentity", "With")
                        .WithMany()
                        .HasForeignKey("WithId");

                    b.Navigation("Me");

                    b.Navigation("With");
                });

            modelBuilder.Entity("SocialNetwork.Models.ChatModels.Message", b =>
                {
                    b.HasOne("SocialNetwork.Models.ChatModels.Chat", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Comment", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.Follower", "UserFrom")
                        .WithMany()
                        .HasForeignKey("UserFromId");

                    b.HasOne("SocialNetwork.Models.UserModels.UserPost", null)
                        .WithMany("Comments")
                        .HasForeignKey("UserPostId");

                    b.Navigation("UserFrom");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Follower", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.UserAccount", null)
                        .WithMany("RecentlyUsers")
                        .HasForeignKey("UserAccountId");

                    b.HasOne("SocialNetwork.Models.UserModels.UserAccount", null)
                        .WithMany("UserFollowers")
                        .HasForeignKey("UserAccountId1");

                    b.HasOne("SocialNetwork.Models.UserModels.UserAccount", null)
                        .WithMany("UserFollowing")
                        .HasForeignKey("UserAccountId2");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.ImagePost", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.UserPost", null)
                        .WithMany("Images")
                        .HasForeignKey("UserPostId");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Like", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.Comment", null)
                        .WithMany("Likes")
                        .HasForeignKey("CommentId");

                    b.HasOne("SocialNetwork.Models.ChatModels.Message", null)
                        .WithMany("Likes")
                        .HasForeignKey("MessageId");

                    b.HasOne("SocialNetwork.Models.UserModels.UserPost", null)
                        .WithMany("Likes")
                        .HasForeignKey("UserPostId");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserAccount", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.UserIdentity", "UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserIdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialNetwork.Models.UserModels.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserIdentity");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserIdentity", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserInfo", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.UserAccount", null)
                        .WithMany("FriendRequests")
                        .HasForeignKey("UserAccountId");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserPost", b =>
                {
                    b.HasOne("SocialNetwork.Models.UserModels.UserAccount", "From")
                        .WithMany("UserPosts")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");
                });

            modelBuilder.Entity("SocialNetwork.Models.ChatModels.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("SocialNetwork.Models.ChatModels.Message", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Comment", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserAccount", b =>
                {
                    b.Navigation("FriendRequests");

                    b.Navigation("RecentlyUsers");

                    b.Navigation("UserChats");

                    b.Navigation("UserFollowers");

                    b.Navigation("UserFollowing");

                    b.Navigation("UserPosts");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserModels.UserPost", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
