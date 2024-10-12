using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Migrations
{
    public partial class MergeTablesIntoProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Departments_OwnerDepartmentID",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ProcessCategories_CategoryID",
                table: "Processes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ProcessCategories");

            migrationBuilder.DropIndex(
                name: "IX_Processes_CategoryID",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_OwnerDepartmentID",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "OwnerDepartmentID",
                table: "Processes");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerDepartmentID",
                table: "Processes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "ProcessCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_CategoryID",
                table: "Processes",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_OwnerDepartmentID",
                table: "Processes",
                column: "OwnerDepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Departments_OwnerDepartmentID",
                table: "Processes",
                column: "OwnerDepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ProcessCategories_CategoryID",
                table: "Processes",
                column: "CategoryID",
                principalTable: "ProcessCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
