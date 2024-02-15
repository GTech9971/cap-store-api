using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cap_store_api.Migrations
{
    /// <inheritdoc />
    public partial class fixakizukiorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slip_number",
                table: "orders");

            migrationBuilder.AlterColumn<string>(
                name: "catalog_id",
                table: "order_details",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "slip_number",
                table: "orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "catalog_id",
                table: "order_details",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(6)",
                oldMaxLength: 6);
        }
    }
}
