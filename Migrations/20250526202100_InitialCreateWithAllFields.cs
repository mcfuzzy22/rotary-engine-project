using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rotaryproject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithAllFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PartCate__19093A2B0B380B12", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PieceCount = table.Column<int>(type: "int", nullable: true),
                    SizeMm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ManufacturingYear = table.Column<int>(type: "int", nullable: true),
                    SealAmount = table.Column<int>(type: "int", nullable: true),
                    EngineCompatibility = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    RatingCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parts__7C3F0D3095AB3286", x => x.PartId);
                    table.ForeignKey(
                        name: "FK__Parts__CategoryI__3B75D760",
                        column: x => x.CategoryId,
                        principalTable: "PartCategories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "CompatibilityRules",
                columns: table => new
                {
                    RuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartA_ID = table.Column<int>(type: "int", nullable: false),
                    PartB_ID = table.Column<int>(type: "int", nullable: false),
                    IsCompatible = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compatib__110458C2D19B7CFD", x => x.RuleID);
                    table.ForeignKey(
                        name: "FK__Compatibi__PartA__4316F928",
                        column: x => x.PartA_ID,
                        principalTable: "Parts",
                        principalColumn: "PartId");
                    table.ForeignKey(
                        name: "FK__Compatibi__PartB__440B1D61",
                        column: x => x.PartB_ID,
                        principalTable: "Parts",
                        principalColumn: "PartId");
                });

            migrationBuilder.CreateTable(
                name: "PartStats",
                columns: table => new
                {
                    PartStatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartID = table.Column<int>(type: "int", nullable: false),
                    StatName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StatValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PartStat__AFCC32BA505FA52A", x => x.PartStatID);
                    table.ForeignKey(
                        name: "FK__PartStats__PartI__3E52440B",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompatibilityRules_PartA_ID",
                table: "CompatibilityRules",
                column: "PartA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CompatibilityRules_PartB_ID",
                table: "CompatibilityRules",
                column: "PartB_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__Compatib__C9D6D737D786A5C9",
                table: "CompatibilityRules",
                columns: new[] { "PartA_ID", "PartB_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__PartCate__737584F67434BF54",
                table: "PartCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_CategoryId",
                table: "Parts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartStats_PartID",
                table: "PartStats",
                column: "PartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompatibilityRules");

            migrationBuilder.DropTable(
                name: "PartStats");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "PartCategories");
        }
    }
}
