namespace exam9kassymovdaniyar.ViewModels.User;

public class ReviewVm
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public int Rating { get; set; }
    
    public Models.Restaurant? Restaurant { get; set; }
    public int RestaurantId { get; set; }

    public Models.User? User { get; set; }
    public required string UserId { get; set; }
}