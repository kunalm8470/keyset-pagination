using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly List<Post> _posts;

        public PostsController(IRandomPostService randomPostService)
        {
            _posts = randomPostService.GeneratedRandomPosts();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts(
            [FromQuery] int limit = 50,
            [FromQuery] DateTime? searchAfter = default
        )
        {
            if (limit > 50)
            {
                ModelState.Clear();

                ModelState.AddModelError(nameof(limit), "Cannot request more than 50 items at a time!");

                return ValidationProblem(ModelState);
            }

            if (searchAfter is null)
            {
                IEnumerable<Post> initialPosts = _posts
                .Take(limit)
                .OrderBy(x => x.CreatedAt);

                return Ok(initialPosts);
            }

            IEnumerable<Post> filtered = _posts
            .Where(x => x.CreatedAt > searchAfter.Value)
            .Take(limit)
            .OrderBy(x => x.CreatedAt);

            return Ok(filtered);
        }
    }
}
