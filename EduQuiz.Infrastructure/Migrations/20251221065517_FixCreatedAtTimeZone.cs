using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduQuiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatedAtTimeZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "student_quizzes",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "quizzes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "quizzes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "quizzes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "questions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "classes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MarkedAt",
                table: "attendances",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "accounts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "student_quizzes",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "quizzes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "quizzes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "quizzes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "questions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "classes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MarkedAt",
                table: "attendances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");
        }
    }
}
