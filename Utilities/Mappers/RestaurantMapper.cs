using exam9kassymovdaniyar.Models;
using exam9kassymovdaniyar.ViewModels.Restaurant;

namespace exam9kassymovdaniyar.Utilities.Mappers;

public static class RestaurantMapper
{
    public static Restaurant AddRestaurantVmRestaurant(AddRestaurantVm vm, string image)
    {
        return new Restaurant
        {
            Title = vm.Title,
            Image = image,
            Description = vm.Description,
        };
    }

    public static RestaurantVm RestaurantRestaurantVm(Restaurant restaurant)
    {
        return new RestaurantVm
        {
            Id = restaurant.Id,
            Title = restaurant.Title,
            Image = restaurant.Image,
            Gallery = restaurant.Gallery,
            Reviews = restaurant.Reviews
        };
    }

    public static RestaurantDetailsVm RestaurantRestaurantDetailsVm(Restaurant restaurant)
    {
        return new RestaurantDetailsVm
        {
            Id = restaurant.Id,
            Title = restaurant.Title,
            Image = restaurant.Image,
            Description = restaurant.Description,
            Gallery = restaurant.Gallery,
            Reviews = restaurant.Reviews
        };
    }
}