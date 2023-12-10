using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace cap_store_api.Migrations
{
    /// <inheritdoc />
    public partial class addcomponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "components",
                columns: table => new
                {
                    component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    model_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    maker_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_components", x => x.component_id);
                    table.ForeignKey(
                        name: "FK_components_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_components_makers_maker_id",
                        column: x => x.maker_id,
                        principalTable: "makers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "component_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    component_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    ComponentDataComponentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_component_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_component_images_components_ComponentDataComponentId",
                        column: x => x.ComponentDataComponentId,
                        principalTable: "components",
                        principalColumn: "component_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_component_images_ComponentDataComponentId",
                table: "component_images",
                column: "ComponentDataComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_components_category_id",
                table: "components",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_components_maker_id",
                table: "components",
                column: "maker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "component_images");

            migrationBuilder.DropTable(
                name: "components");
        }
    }
}
