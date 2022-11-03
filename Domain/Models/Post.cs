namespace Domain.Models;

public class Post
{
    public int  Id { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }

    public User Creator { get; set; }

    public Post(string title, string? body, User creator)
    {
        Title = title;
        Body = body;
        Creator = creator;
    }
}