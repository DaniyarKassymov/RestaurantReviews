using System.ComponentModel.DataAnnotations;
using exam9kassymovdaniyar.Database;
using exam9kassymovdaniyar.Models;
using exam9kassymovdaniyar.Utilities.Enums;
using exam9kassymovdaniyar.Utilities.Mappers;
using exam9kassymovdaniyar.Utilities.Services;
using exam9kassymovdaniyar.ViewModels.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam9kassymovdaniyar.Controllers;

public class RestaurantController : Controller
{
    private readonly DatabaseContext _db;
    private readonly UserManager<User> _userManager;

    public RestaurantController(DatabaseContext db, UserManager<User> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    [Display(Name = "AllRestaurants")]
    public async Task<IActionResult> AllRestaurantsAsync(int page = 1)
    {
        var pageSize = 10;
        
        var restaurants = await _db.Restaurants.Include(r => r.Reviews).ToListAsync();
        var restaurantVms = restaurants
            .Select(RestaurantMapper.RestaurantRestaurantVm)
            .ToList();

        var count = restaurantVms.Count;
        var items = restaurantVms.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var paginationVm = new RestaurantPaginationVm(count, page, pageSize);

        var vm = new AllRestaurantsVm
        {
            Restaurants = items,
            PaginationVm = paginationVm
        };

        return View(vm);
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "AddRestaurant")]
    public async Task<IActionResult> AddRestaurantAsync()
    {
        var vm = new AddRestaurantVm
        {
            Title = null,
            Image = null,
            Description = null
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Display(Name = "AddRestaurant")]
    public async Task<IActionResult> AddRestaurantAsync(AddRestaurantVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var image = await FileUpload.Upload(vm.Title, vm.Image);
        var restaurant = RestaurantMapper.AddRestaurantVmRestaurant(vm, image);
        await _db.Restaurants.AddAsync(restaurant);
        await _db.SaveChangesAsync();

        return RedirectToAction("AllRestaurants");
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Display(Name = "FindRestaurant")]
    public async Task<IActionResult> FindRestaurantAsync(string searchKey)
    {
        var restaurants = await _db.Restaurants
            .Where(r => r.Title.Contains(searchKey) || r.Description.Contains(searchKey))
            .ToListAsync();

        var restaurantVms = restaurants
            .Select(RestaurantMapper.RestaurantRestaurantVm)
            .ToList();

        return View(restaurantVms);
    }

    [HttpGet]
    [AllowAnonymous]
    [Display(Name = "RestaurantDetails")]
    public async Task<IActionResult> RestaurantDetailsAsync(int id)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Reviews)
            .FirstOrDefaultAsync(r => r.Id == id);

        await _db.Reviews
            .Include(r => r.User)
            .ToListAsync();
        
        if (restaurant is not null)
        {
            var vm = RestaurantMapper.RestaurantRestaurantDetailsVm(restaurant);
            ViewBag.UserId = _userManager.GetUserId(User);
            ViewBag.Rating = new List<Rating>
            {
                Rating.One,
                Rating.Two,
                Rating.Three,
                Rating.Four,
                Rating.Five,
            };

            return View(vm);
        }

        return NotFound();
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "DeleteReview")]
    public async Task<IActionResult> DeleteReviewAsync(int restaurantId)
    {
        var review = await _db.Reviews
            .FirstOrDefaultAsync(r => r.UserId == _userManager.GetUserId(User) 
                                      && r.RestaurantId == restaurantId);

        if (review is not null)
        {
            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();

            return RedirectToAction("RestaurantDetails", new { id = restaurantId });
        }

        return NotFound();
    }
    
    public async Task<ActionResult> Create([FromForm] CreateReviewDto dto)
    {
        var relativePath = await FileUpload.Upload(Guid.NewGuid().ToString(), dto.Photo);
        
        var restaurant = await _db.Restaurants.FindAsync(dto.RestaurantId);
        restaurant.Gallery.Add(relativePath);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            relativePath
        });
    }
}