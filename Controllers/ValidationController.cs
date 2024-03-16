using exam9kassymovdaniyar.Database;
using Microsoft.AspNetCore.Mvc;

namespace exam9kassymovdaniyar.Controllers;

public class ValidationController : Controller
{
    private readonly DatabaseContext _db;

    public ValidationController(DatabaseContext db)
    {
        _db = db;
    }

    public bool CheckUserNameOrEmail(string emailOrUserName) => 
        _db.Users.Any(u => u.Email == emailOrUserName 
        || u.UserName == emailOrUserName);
    
    public bool CheckUserName(string userName) => !_db.Users.Any(u => u.UserName == userName); 
    
    public bool CheckEmail(string email) => !_db.Users.Any(u => u.Email == email);
    
    public bool CheckRestaurantTitle(string title) => !_db.Restaurants.Any(r => r.Title == title);
}