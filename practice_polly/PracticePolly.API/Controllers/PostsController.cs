using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticePolly.API.Services;

namespace PracticePolly.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
}
