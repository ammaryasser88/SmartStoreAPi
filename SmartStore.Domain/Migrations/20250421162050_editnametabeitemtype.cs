using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartStore.Domain.Migrations
{
    /// <inheritdoc />
    public partial class editnametabeitemtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemGroups_GroupId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemGroups",
                table: "ItemGroups");

            migrationBuilder.RenameTable(
                name: "ItemGroups",
                newName: "ItemTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTypes",
                table: "ItemTypes",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_GroupId",
                table: "Items",
                column: "TypeId",
                principalTable: "ItemTypes",
                principalColumn: "ItemTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_GroupId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTypes",
                table: "ItemTypes");

            migrationBuilder.RenameTable(
                name: "ItemTypes",
                newName: "ItemGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemGroups",
                table: "ItemGroups",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemGroups_GroupId",
                table: "Items",
                column: "TypeId",
                principalTable: "ItemGroups",
                principalColumn: "ItemTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
