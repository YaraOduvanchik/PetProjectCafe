using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProjectCafe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_column_quantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "order_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "order_items");
        }
    }
}
