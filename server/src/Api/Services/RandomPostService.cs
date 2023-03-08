using Api.Interfaces;
using Api.Models;
using Bogus;

namespace Api.Services;

public class RandomPostService : IRandomPostService
{
    public List<Post> GeneratedRandomPosts(int limit = 1000)
    {
        DateTime startRange = new(2000, 1, 1);
        DateTime endRange = new(2009, 12, 31);

        Faker<Post> posts = new Faker<Post>()
        .RuleFor(p => p.Title, f => f.Lorem.Paragraph(1))
        .RuleFor(p => p.Body, f => f.Lorem.Paragraph(20))
        .RuleFor(p => p.Author, f => f.Person.UserName)
        .RuleFor(s => s.CreatedAt, f => f.Date.Between(startRange, endRange).ToUniversalTime());

        return posts.GenerateLazy(limit)
        .OrderBy(x => x.CreatedAt)
        .ToList();
    }
}
