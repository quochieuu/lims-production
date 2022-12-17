using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoanDebtCat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "LoanDebts");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "LoanDebts",
                newName: "Duration");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "LoanDebts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoanDebtCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDebtCategories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "570e22a4-8a35-45fd-904e-2f74681aa2d4", "AQAAAAIAAYagAAAAECPAFeI0l43wM8xllDud+M+1Goj9WMWiDajKPkHLKl35injK9j38w71aqd7tHp34fQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDebtCategories");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "LoanDebts");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "LoanDebts",
                newName: "Deadline");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LoanDebts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d12f6e83-e215-4580-8558-4b79da9b606e", "AQAAAAIAAYagAAAAEIvAHW/7bicKfVYmKegHCyjQKqicRLJ5xZbpaNFGLRokVhqmLbVQvF8+3mgM5Z/vPQ==" });
        }
    }
}
