namespace Domain.DTOs;

public class SearchParametersDto
{
    public string? UsernameContains { get; }

    public SearchParametersDto(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}