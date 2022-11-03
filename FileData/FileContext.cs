using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return _dataContainer!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _dataContainer!.Users;
        }
    }

    private void LoadData()
    {
        if (_dataContainer != null) return;

        if (!File.Exists(filePath))
        {
            _dataContainer = new()
            {
                Posts = new List<Post>(),
                Users = new List<User>()
            };
            return;
        }

        string content = File.ReadAllText(filePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer);
        File.WriteAllText(filePath, serialized);
        _dataContainer = null;
    }
}