namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }

    public UserCreationDto(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
}