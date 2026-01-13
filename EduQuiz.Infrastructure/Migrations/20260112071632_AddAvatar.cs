using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduQuiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentPhoneNumer",
                table: "students",
                newName: "ParentPhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "ParentPhoneNumber",
                table: "students",
                newName: "ParentPhoneNumer");
        }
    }
}
