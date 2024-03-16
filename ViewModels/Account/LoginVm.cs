using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace exam9kassymovdaniyar.ViewModels.Account;

public class LoginVm
{
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [Remote("CheckUserNameOrEmail", "Validation", ErrorMessage = "Вы ввели неверные данные")]
    public required string EmailOrUserName { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}