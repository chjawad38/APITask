using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerCollectionApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingCountToBeer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Beers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Beers");
        }
    }
}
