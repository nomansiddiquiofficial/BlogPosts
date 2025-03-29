using System;
using System.Collections.Generic;

namespace BlogPost.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime PusblishedAt { get; set; }

    public string? AuthorName { get; set; }
}
