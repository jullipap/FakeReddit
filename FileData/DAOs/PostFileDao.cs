using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext _context;

    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (_context.Posts.Any())
        {
            id = _context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        
        _context.Posts.Add(post);
        _context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        IEnumerable<Post> result = _context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchPostParametersDto.Username))
        {
            result = _context.Posts.Where(post =>
                post.Creator.UserName.Equals(searchPostParametersDto.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchPostParametersDto.UserId != null)
        {
            result = result.Where(p => p.Creator.Id == searchPostParametersDto.UserId);
        }

        if (!string.IsNullOrEmpty(searchPostParametersDto.TitleContains))
        {
            result = result.Where(p =>
                p.Title.Contains(searchPostParametersDto.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(result);
    }

    public Task<Post?> GetByPostIdAsync(int id)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(existing);
    }
}