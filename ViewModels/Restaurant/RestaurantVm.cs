using exam9kassymovdaniyar.Models;

namespace exam9kassymovdaniyar.ViewModels.Restaurant;

public class RestaurantVm
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Image { get; set; }
    public List<string>? Gallery { get; set; } = new();

    public List<Review> Reviews { get; set; } = new();
}