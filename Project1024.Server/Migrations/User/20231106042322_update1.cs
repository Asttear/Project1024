using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1024.Server.Migrations.User
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                table: "user",
                type: "varchar(511)",
                maxLength: 511,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nickname",
                table: "user",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "signature",
                table: "user",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar_url",
                table: "user");

            migrationBuilder.DropColumn(
                name: "nickname",
                table: "user");

            migrationBuilder.DropColumn(
                name: "signature",
                table: "user");
        }
    }
}
