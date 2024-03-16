using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace exam9kassymovdaniyar.ViewModels.Restaurant;

public class AddRestaurantVm
{
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [Remote("CheckRestaurantTitle", "Validation", ErrorMessage = "*Заведение с таким названием уже существует")]
    public required string Title { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [DataType(DataType.Upload)]
    public required IFormFile Image { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    public required string Description { get; set; }
}