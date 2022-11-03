using Domain.Models;

namespace Domain.DTOs;

public class PostCreationDto
{
    public string Title { get; }
    public string Body { get; }

    public int UserId { get; }

    public PostCreationDto(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }
}