using PracticePolly.API.Models;
using System.Text.Json;

namespace PracticePolly.API.Services;

public class PostService : IPostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        var response = await _httpClient.GetAsync("posts");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var posts = JsonSerializer.Deserialize<List<Post>>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });

        return posts ?? new List<Post>();
    }
}