using exam9kassymovdaniyar.Models;
using exam9kassymovdaniyar.ViewModels.User;

namespace exam9kassymovdaniyar.Utilities.Mappers;

public static class UserMapper
{
    public static ReviewVm ReviewReviewVm(Review review)
    {
        return new ReviewVm
        {
            Id = review.Id,
            CreatedAt = review.CreatedAt,
            Description = review.Description,
            Rating = review.Rating,
            Restaurant = review.Restaurant,
            RestaurantId = review.RestaurantId,
            User = review.User,
            UserId = review.UserId
        };
    }
}