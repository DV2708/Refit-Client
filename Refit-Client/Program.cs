using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using Refit_Client;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services
            .AddRefitClient<IPostsClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
    }).Build();

var postsClient = host.Services.GetRequiredService<IPostsClient>();

// Get All Post
Console.WriteLine(">> >> >> Get All Post >> >> >>");
var posts = await postsClient.GetAll();

foreach (var post in posts)
{
    Console.WriteLine(post.Title);
}
Console.WriteLine(">> >> >> Get All Post ended >> >> >>" + Environment.NewLine);

// Create
Console.WriteLine(">> >> >> Create a new post >> >> >>");
var newPost = new Post
{
    Title = "New Post",
    Body = "New Post",
    UserId = 1
};

var newPostId = (await postsClient.CreatePost(newPost)).Id;
Console.WriteLine($"Post with Id: {newPostId} created");
Console.WriteLine(">> >> >> Post created >> >> >>" + Environment.NewLine);

// Update
Console.WriteLine(">> >> >> Update a post >> >> >>");
var existingPost = await postsClient.GetPost(1);
existingPost.Title = "New Title is given";
var updatedPost = await postsClient.UpdatePost(existingPost.Id, existingPost);
Console.WriteLine($"Post Title updated to {updatedPost.Title}");
Console.WriteLine(">> >> >> Post Updated >> >> >>" + Environment.NewLine);

// Delete
Console.WriteLine(">> >> >> Delete a post >> >> >>");
await postsClient.DeletePost(newPostId);
Console.WriteLine(">> Post Deleted");

Console.Read();
