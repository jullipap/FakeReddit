using Domain.Models;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);

    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto);

    Task<Post> GetPostByIdAsync(int id);
}