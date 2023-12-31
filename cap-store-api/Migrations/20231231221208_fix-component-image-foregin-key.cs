using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cap_store_api.Migrations
{
    /// <inheritdoc />
    public partial class fixcomponentimageforeginkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_component_images_components_ComponentDataComponentId",
                table: "component_images");

            migrationBuilder.DropIndex(
                name: "IX_component_images_ComponentDataComponentId",
                table: "component_images");

            migrationBuilder.DropColumn(
                name: "ComponentDataComponentId",
                table: "component_images");

            migrationBuilder.RenameColumn(
                name: "component_id",
                table: "component_images",
                newName: "component_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_component_images_component_id_fk",
                table: "component_images",
                column: "component_id_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_component_images_components_component_id_fk",
                table: "component_images",
                column: "component_id_fk",
                principalTable: "components",
                principalColumn: "component_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_component_images_components_component_id_fk",
                table: "component_images");

            migrationBuilder.DropIndex(
                name: "IX_component_images_component_id_fk",
                table: "component_images");

            migrationBuilder.RenameColumn(
                name: "component_id_fk",
                table: "component_images",
                newName: "component_id");

            migrationBuilder.AddColumn<int>(
                name: "ComponentDataComponentId",
                table: "component_images",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_component_images_ComponentDataComponentId",
                table: "component_images",
                column: "ComponentDataComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_component_images_components_ComponentDataComponentId",
                table: "component_images",
                column: "ComponentDataComponentId",
                principalTable: "components",
                principalColumn: "component_id");
        }
    }
}
