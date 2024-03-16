using exam9kassymovdaniyar.Database;
using exam9kassymovdaniyar.Models;
using exam9kassymovdaniyar.Utilities.Enums;
using exam9kassymovdaniyar.Utilities.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam9kassymovdaniyar.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly DatabaseContext _db;

    public UserController(UserManager<User> userManager, DatabaseContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> AddReview(string? review, int restaurantId, Rating rating)
    {
        if (review == null) return NotFound();

        if (_db.Reviews.Any(r => r.RestaurantId == restaurantId &&
                                 r.UserId == _userManager.GetUserId(User)))
            return BadRequest("Вы уже добавляли коментарий");
        
        var reviewInDb = new Review 
        {
            Description = review,
            CreatedAt = DateTime.UtcNow,
            UserId = _userManager.GetUserId(User),
            RestaurantId = restaurantId,
            Rating = (int) rating
        };

        await _db.Reviews.AddAsync(reviewInDb);
        await _db.SaveChangesAsync();

        var lastAnswer = await _db.Reviews
            .Include(a => a.User)
            .Include(a => a.Restaurant)
            .OrderBy(a => a.Id)
            .LastOrDefaultAsync();


        var vm = UserMapper.ReviewReviewVm(lastAnswer);

        return PartialView(vm);
    }
}