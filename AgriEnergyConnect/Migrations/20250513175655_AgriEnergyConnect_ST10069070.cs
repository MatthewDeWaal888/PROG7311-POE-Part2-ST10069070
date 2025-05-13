using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergyConnect.Migrations
{
    /// <inheritdoc />
    public partial class AgriEnergyConnect_ST10069070 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscussionId",
                table: "Discussion",
                newName: "DiscussionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscussionID",
                table: "Discussion",
                newName: "DiscussionId");
        }
    }
}
