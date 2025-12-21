using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduQuiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClassStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "classes");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:class_status", "DRAFT,PUBLISHED,UNPUBLISHED,ONGOING,REMOVED");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "classes",
                type: "class_status",
                nullable: false,
                defaultValue: "DRAFT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "classes");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:class_status", "DRAFT,PUBLISHED,UNPUBLISHED,ONGOING,REMOVED");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "classes",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }
    }
}
