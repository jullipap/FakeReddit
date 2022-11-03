using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);

    Task<User?> GetByIdAsync(int id);
    
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto);

    Task<Post> GetByPostIdAsync(int id); 
}