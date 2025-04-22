using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerCollectionApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAverageRatingToBeer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Beers",
                type: "float",
                nullable: true);

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
                name: "AverageRating",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Beers");
        }
    }
}
