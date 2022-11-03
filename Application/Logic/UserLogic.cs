using System.Text.RegularExpressions;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await _userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateUsername(dto);
        ValidatePassword(dto);
        ValidateEmail(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Password = dto.Password
        };

        User created = await _userDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidateUsername(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
        
        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");
        
        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }

    private static void ValidateEmail(UserCreationDto userToCreate)
    {
        string email = userToCreate.Email;
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        
        if (!emailRegex.IsMatch(email)) 
            throw new Exception("Invalid email!");
    }

    private static void ValidatePassword(UserCreationDto userToCreate)
    {
        string password = userToCreate.Password;

        if (password.Length < 5)
            throw new Exception("Password must have 5 or more characters!");
    }
}