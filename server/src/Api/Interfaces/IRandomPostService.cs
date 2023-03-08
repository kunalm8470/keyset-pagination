using Api.Models;

namespace Api.Interfaces;

public interface IRandomPostService
{
    public List<Post> GeneratedRandomPosts(int limit = 1000);
}
