using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Infrastructure.Migrations
{
    public partial class tbl_products_parameters_resources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "lower(newid())"),
                    CreatedUTC = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 23, 17, 58, 50, 502, DateTimeKind.Utc).AddTicks(130)),
                    ModifiedUTC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUTC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "lower(newid())"),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReleaseUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Releases");
        }
    }
}
