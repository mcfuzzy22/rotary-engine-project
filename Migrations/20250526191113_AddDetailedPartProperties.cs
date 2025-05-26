using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rotaryproject.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailedPartProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Parts_Name",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "UQ__Parts__CA1ECF0DDFFFB887",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "SKU",
                table: "Parts",
                newName: "Sku");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Parts",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "PartID",
                table: "Parts",
                newName: "PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_CategoryID",
                table: "Parts",
                newName: "IX_Parts_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Parts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineCompatibility",
                table: "Parts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturingYear",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Parts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PieceCount",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Parts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SealAmount",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeMm",
                table: "Parts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "EngineCompatibility",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ManufacturingYear",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PieceCount",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "SealAmount",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "SizeMm",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "Sku",
                table: "Parts",
                newName: "SKU");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Parts",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "PartId",
                table: "Parts",
                newName: "PartID");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_CategoryId",
                table: "Parts",
                newName: "IX_Parts_CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_Name",
                table: "Parts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "UQ__Parts__CA1ECF0DDFFFB887",
                table: "Parts",
                column: "SKU",
                unique: true,
                filter: "[SKU] IS NOT NULL");
        }
    }
}
