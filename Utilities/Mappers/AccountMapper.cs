using exam9kassymovdaniyar.Models;
using exam9kassymovdaniyar.ViewModels.Account;

namespace exam9kassymovdaniyar.Utilities.Mappers;

public static class AccountMapper
{
    public static User RegistrationVmUser(RegistrationVm vm)
    {
        return new User
        {
            UserName = vm.UserName,
            Email = vm.Email
        };
    } 
}