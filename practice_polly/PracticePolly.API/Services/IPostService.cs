using PracticePolly.API.Models;

namespace PracticePolly.API.Services;

public interface IPostService
{
    Task<List<Post>> GetPostsAsync();
}
