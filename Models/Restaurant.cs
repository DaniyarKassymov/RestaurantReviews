namespace exam9kassymovdaniyar.Models;

public class Restaurant
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Image { get; set; }
    public required string Description { get; set; }
    public List<string>? Gallery { get; set; } = new();

    public List<Review> Reviews { get; set; } = new();
}