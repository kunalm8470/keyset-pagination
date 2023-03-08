using Web.Models;

namespace Web.Services;

public interface IPostService
{
    public Task<IEnumerable<Post>> FetchPostsAsync(int limit = 50, DateTime? searchAfter = default); 
}
