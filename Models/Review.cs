namespace exam9kassymovdaniyar.Models;

public class Review
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public int Rating { get; set; }
    
    public Restaurant? Restaurant { get; set; }
    public int RestaurantId { get; set; }

    public User? User { get; set; }
    public required string UserId { get; set; }
}