﻿namespace Api.Models;

public class Post
{
    public string Title { get; set; }

    public string Body { get; set; }

    public string Author { get; set; }

    public DateTime CreatedAt { get; set; }
}
