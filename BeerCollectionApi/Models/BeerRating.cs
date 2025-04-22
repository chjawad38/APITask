namespace BeerCollectionAPI.Models;

public class BeerRating
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public Beer Beer { get; set; } = null!;
    public double Score { get; set; }
    public DateTime RatedOn { get; set; } = DateTime.UtcNow;
}