namespace BeerCollectionAPI.DTOs;

public class BeerDetailedDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double? AverageRating { get; set; }
    public int RatingCount { get; set; }
    public List<RatingDetailDTO> Ratings { get; set; } = new();
}

public class RatingDetailDTO
{
    public double Score { get; set; }
    public DateTime RatedOn { get; set; }
}