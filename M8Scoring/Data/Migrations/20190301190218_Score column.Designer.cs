﻿// <auto-generated />
using System;
using M8Scoring.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace M8Scoring.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190301190218_Score column")]
    partial class Scorecolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("M8Scoring.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("Flags");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Notes");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("SubscriptionId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int>("Type");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SubscriptionId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("M8Scoring.Data.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsRegularSeason");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Location");

                    b.Property<int>("NonRatedPlayerRate");

                    b.Property<int>("Number");

                    b.Property<int>("OpponentBonusOrPenalty");

                    b.Property<int?>("OpponentId");

                    b.Property<int>("OverUnderPenalty");

                    b.Property<int>("PenaltyMultiplier");

                    b.Property<int>("TeamBonusOrPenalty");

                    b.Property<int?>("TeamId");

                    b.Property<int>("TotalOpponentRate");

                    b.Property<int>("TotalOpponentScore");

                    b.Property<int>("TotalRate");

                    b.Property<int>("TotalScore");

                    b.Property<int>("WinMultiplier");

                    b.HasKey("Id");

                    b.HasIndex("OpponentId");

                    b.HasIndex("TeamId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("M8Scoring.Data.MatchSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MatchId");

                    b.Property<int>("SetNumber");

                    b.Property<bool>("Win");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchSets");
                });

            modelBuilder.Entity("M8Scoring.Data.MatchSetPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Forfeit");

                    b.Property<int>("MatchSetId");

                    b.Property<int>("Number");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Rate");

                    b.HasKey("Id");

                    b.HasIndex("MatchSetId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchSetPlayers");
                });

            modelBuilder.Entity("M8Scoring.Data.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("M8Scoring.Data.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("M8Scoring.Data.SubscriptionTeam", b =>
                {
                    b.Property<int>("SubscriptionId");

                    b.Property<int>("TeamId");

                    b.Property<string>("TeamLogin");

                    b.Property<string>("TeamPassword");

                    b.HasKey("SubscriptionId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("SubscriptionTeam");
                });

            modelBuilder.Entity("M8Scoring.Data.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("M8Scoring.Data.TeamPlayer", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("PlayerId");

                    b.HasKey("TeamId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("M8Scoring.Data.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("Type");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("M8Scoring.Data.ApplicationUser", b =>
                {
                    b.HasOne("M8Scoring.Data.Subscription", "Subscription")
                        .WithOne("User")
                        .HasForeignKey("M8Scoring.Data.ApplicationUser", "SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("M8Scoring.Data.Match", b =>
                {
                    b.HasOne("M8Scoring.Data.Team", "Opponent")
                        .WithMany()
                        .HasForeignKey("OpponentId");

                    b.HasOne("M8Scoring.Data.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("M8Scoring.Data.MatchSet", b =>
                {
                    b.HasOne("M8Scoring.Data.Match", "Match")
                        .WithMany("Sets")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("M8Scoring.Data.MatchSetPlayer", b =>
                {
                    b.HasOne("M8Scoring.Data.MatchSet", "MatchSet")
                        .WithMany("Players")
                        .HasForeignKey("MatchSetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("M8Scoring.Data.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("M8Scoring.Data.SubscriptionTeam", b =>
                {
                    b.HasOne("M8Scoring.Data.Subscription", "Subscription")
                        .WithMany("SubscriptionTeams")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("M8Scoring.Data.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("M8Scoring.Data.TeamPlayer", b =>
                {
                    b.HasOne("M8Scoring.Data.Player", "Player")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("M8Scoring.Data.Team", "Team")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("M8Scoring.Data.Token", b =>
                {
                    b.HasOne("M8Scoring.Data.ApplicationUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("M8Scoring.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("M8Scoring.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("M8Scoring.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("M8Scoring.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
