using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Categories_CategoryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Locations_LocationId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "Label",
                table: "Categories",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Machines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Machines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Categories_CategoryId",
                table: "Machines",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Locations_LocationId",
                table: "Machines",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Categories_CategoryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Locations_LocationId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Label");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Machines",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Machines",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Categories_CategoryId",
                table: "Machines",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Locations_LocationId",
                table: "Machines",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
