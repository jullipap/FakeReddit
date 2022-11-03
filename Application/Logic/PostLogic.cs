using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{

    private readonly IPostDao postdao;
    private readonly IUserDao userdao;

    public PostLogic(IPostDao postdao, IUserDao userdao)
    {
        this.postdao = postdao;
        this.userdao = userdao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userdao.GetByIdAsync(dto.UserId);
        if (user ==null)
        {
            throw new Exception($"User with id {dto.UserId} was not found!");
        }
        
        ValidatePost(dto);
        Post post = new Post(dto.Title, dto.Body, user);
        Post created = await postdao.CreateAsync(post);
        return created;

    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        return postdao.GetAsync(searchPostParametersDto);
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        Post? post = await postdao.GetByPostIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found!");
        }

        return post;
    }

    public void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty");

    }
}