using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rotaryproject.Migrations
{
    /// <inheritdoc />
    public partial class AddSelfReferencingToSubCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentEngineSubCategoryId",
                table: "EngineSubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngineSubCategories_ParentEngineSubCategoryId",
                table: "EngineSubCategories",
                column: "ParentEngineSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineSubCategories_EngineSubCategories_ParentEngineSubCategoryId",
                table: "EngineSubCategories",
                column: "ParentEngineSubCategoryId",
                principalTable: "EngineSubCategories",
                principalColumn: "EngineSubCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineSubCategories_EngineSubCategories_ParentEngineSubCategoryId",
                table: "EngineSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_EngineSubCategories_ParentEngineSubCategoryId",
                table: "EngineSubCategories");

            migrationBuilder.DropColumn(
                name: "ParentEngineSubCategoryId",
                table: "EngineSubCategories");
        }
    }
}
