using System.ComponentModel.DataAnnotations;
using exam9kassymovdaniyar.Database;
using exam9kassymovdaniyar.Models;
using exam9kassymovdaniyar.Utilities.Mappers;
using exam9kassymovdaniyar.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam9kassymovdaniyar.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly DatabaseContext _db;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Registration()
    {
        var vm = new RegistrationVm
        {
            UserName = null,
            Email = null,
            Password = null,
            ConfirmPassword = null
        };
        
        return View(vm);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Display(Name = "Registration")]
    public async Task<IActionResult> RegistrationAsync(RegistrationVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var user = AccountMapper.RegistrationVmUser(vm);
        var userCreateResult = await _userManager.CreateAsync(user, vm.Password);

        if (userCreateResult.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("AllRestaurants", "Restaurant");
        }

        foreach (var error in userCreateResult.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(vm);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        var vm = new LoginVm
        {
            EmailOrUserName = null,
            Password = null
        };

        return View(vm);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Display(Name = "Login")]
    public async Task<IActionResult> LoginAsync(LoginVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var user = await _userManager.FindByEmailAsync(vm.EmailOrUserName) 
                   ?? await _db.Users.FirstOrDefaultAsync(u => u.UserName == vm.EmailOrUserName);

        var result = await _signInManager.PasswordSignInAsync(
            user!,
            vm.Password,
            false,
            false);

        if (result.Succeeded)
            return RedirectToAction("AllRestaurants", "Restaurant");
            
        ModelState.AddModelError(string.Empty, "Неверно введены данные");

        return View(vm);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Display(Name = "LogOut")]
    public async Task<IActionResult> LogOutAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}