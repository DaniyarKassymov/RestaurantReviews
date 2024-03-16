using System.ComponentModel.DataAnnotations;

namespace exam9kassymovdaniyar.Utilities.Enums;

public enum Rating
{
    [Display(Name = "1")]
    One = 1,
    [Display(Name = "2")]
    Two,
    [Display(Name = "3")]
    Three,
    [Display(Name = "4")]
    Four,
    [Display(Name = "5")]
    Five
}