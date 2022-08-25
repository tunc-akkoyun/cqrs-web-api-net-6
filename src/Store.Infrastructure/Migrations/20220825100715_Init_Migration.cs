using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Infrastructure.Migrations
{
    public partial class Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUTC",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 25, 10, 7, 15, 787, DateTimeKind.Utc).AddTicks(8190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 25, 10, 1, 9, 722, DateTimeKind.Utc).AddTicks(8750));

            migrationBuilder.AlterColumn<Guid>(
                name: "Hash",
                table: "Releases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "lower(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "lower(newid())")
                .Annotation("Relational:ColumnOrder", -1)
                .OldAnnotation("Relational:ColumnOrder", -4);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Releases",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", -2)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", -5)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUTC",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 25, 10, 7, 15, 787, DateTimeKind.Utc).AddTicks(4680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 25, 10, 1, 9, 722, DateTimeKind.Utc).AddTicks(6610));

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Hash",
                table: "Users",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_Hash",
                table: "Releases",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Hash",
                table: "Products",
                column: "Hash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Hash",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Releases_Hash",
                table: "Releases");

            migrationBuilder.DropIndex(
                name: "IX_Products_Hash",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUTC",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 25, 10, 1, 9, 722, DateTimeKind.Utc).AddTicks(8750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 25, 10, 7, 15, 787, DateTimeKind.Utc).AddTicks(8190));

            migrationBuilder.AlterColumn<Guid>(
                name: "Hash",
                table: "Releases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "lower(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "lower(newid())")
                .Annotation("Relational:ColumnOrder", -4)
                .OldAnnotation("Relational:ColumnOrder", -1);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Releases",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", -5)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", -2)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUTC",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 25, 10, 1, 9, 722, DateTimeKind.Utc).AddTicks(6610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 25, 10, 7, 15, 787, DateTimeKind.Utc).AddTicks(4680));
        }
    }
}
