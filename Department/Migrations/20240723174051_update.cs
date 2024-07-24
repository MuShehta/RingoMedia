using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Department.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_DepartmentId",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Department",
                newName: "ParentDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_DepartmentId",
                table: "Department",
                newName: "IX_Department_ParentDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Department_ParentDepartmentId",
                table: "Department",
                column: "ParentDepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_ParentDepartmentId",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "ParentDepartmentId",
                table: "Department",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_ParentDepartmentId",
                table: "Department",
                newName: "IX_Department_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Department_DepartmentId",
                table: "Department",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
