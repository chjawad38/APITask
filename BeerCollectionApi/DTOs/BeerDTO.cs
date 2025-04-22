namespace BeerCollectionAPI.DTOs;

public class BeerDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double? AverageRating { get; set; }
}