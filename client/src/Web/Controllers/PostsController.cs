using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int limit = 50;

            List<Post> posts = (await _postService.FetchPostsAsync(limit)).ToList();

            ViewData["searchAfter"] = posts[^1].CreatedAt;

            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMoreProducts(DateTime? searchAfter = default, int limit = 50)
        {
            List<Post> posts = (await _postService.FetchPostsAsync(limit, searchAfter)).ToList();

            return Json(posts);
        }
    }
}
