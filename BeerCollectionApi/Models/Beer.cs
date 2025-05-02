using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerCollectionAPI.Models;

public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public BeerType Type { get; set; }
    public ICollection<BeerRating> Ratings { get; set; } = new List<BeerRating>();
    public double? AverageRating { get; set; }
    public int RatingCount { get; set; } // Add this back
}

public enum BeerType
{
    [Display(Name = "Pale Ale")]
    PaleAle,
    [Display(Name = "India Pale Ale (IPA)")]
    IPA,
    [Display(Name = "Stout")]
    Stout,
    [Display(Name = "Porter")]
    Porter,
    [Display(Name = "Wheat Beer")]
    WheatBeer,
    [Display(Name = "Pilsner")]
    Pilsner,
    [Display(Name = "Lager")]
    Lager,
    [Display(Name = "Sour Ale")]
    SourAle,
    [Display(Name = "Belgian Ale")]
    BelgianAle,
    [Display(Name = "Brown Ale")]
    BrownAle
}