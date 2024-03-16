namespace exam9kassymovdaniyar.ViewModels.Restaurant;

public class AllRestaurantsVm
{
    public List<RestaurantVm> Restaurants { get; set; } = new();
    public RestaurantPaginationVm? PaginationVm { get; set; }
}