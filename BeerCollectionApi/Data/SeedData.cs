using BeerCollectionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerCollectionAPI.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new BeerCollectionContext(
            serviceProvider.GetRequiredService<DbContextOptions<BeerCollectionContext>>());

        if (context.Beers.Any())
        {
            return;
        }

        context.Beers.AddRange(
    new Beer
    {
        Name = "Pale Ale Classic",
        Type = BeerType.PaleAle,
        AverageRating = 4.2,
        RatingCount = 5
    },
    new Beer
    {
        Name = "Stout Master",
        Type = BeerType.Stout,
        AverageRating = 4.5,
        RatingCount = 3
    },
    new Beer
    {
        Name = "IPA Fresh",
        Type = BeerType.IPA,
        AverageRating = 3.8,
        RatingCount = 7
    }
);



        context.SaveChanges();
    }
}