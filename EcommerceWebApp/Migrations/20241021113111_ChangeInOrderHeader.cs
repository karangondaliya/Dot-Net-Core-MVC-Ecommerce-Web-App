using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OrderHeaders",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "OrderHeaders",
                newName: "Name");
        }
    }
}
