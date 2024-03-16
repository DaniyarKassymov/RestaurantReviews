using Microsoft.AspNetCore.Identity;

namespace exam9kassymovdaniyar.Models;

public class User : IdentityUser
{
    public List<Review> Reviews { get; set; } = new();
}