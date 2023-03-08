using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Services;

public class PostService : IPostService
{
    private readonly HttpClient _client;

    public PostService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Post>> FetchPostsAsync(int limit = 50, DateTime? searchAfter = null)
    {
        Dictionary<string, string> query = new()
        {
            ["limit"] = limit.ToString()
        };

        if (searchAfter is not null)
        {
            query.Add("searchAfter", searchAfter.Value.ToString());
        }

        string uri = QueryHelpers.AddQueryString("/api/Posts", query);

        HttpResponseMessage response = await _client.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        string serializedResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<IEnumerable<Post>>(serializedResponse);
    }
}
