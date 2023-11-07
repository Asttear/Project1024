using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project1024.Server.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "plays",
                table: "video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    video_id = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    like_count = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "like",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    target_id = table.Column<int>(type: "int", nullable: false),
                    like_date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_like", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_follower",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    follower_id = table.Column<int>(type: "int", nullable: false),
                    followed_id = table.Column<int>(type: "int", nullable: false),
                    followed_time = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    is_deleted = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_follower", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "like");

            migrationBuilder.DropTable(
                name: "user_follower");

            migrationBuilder.DropColumn(
                name: "plays",
                table: "video");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "video");
        }
    }
}
