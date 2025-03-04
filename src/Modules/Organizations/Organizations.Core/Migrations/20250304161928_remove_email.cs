using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizations.Core.Migrations
{
    /// <inheritdoc />
    public partial class remove_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Organizations",
                table: "OrganizationMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Organizations",
                table: "OrganizationMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
