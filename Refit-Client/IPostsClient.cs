using Refit;

namespace Refit_Client
{
    public interface IPostsClient
    {
        [Get("/posts")]
        Task<IEnumerable<Post>> GetAll();

        [Get("/posts/{id}")]
        Task<Post> GetPost(int id);

        [Post("/posts")]
        Task<Post> CreatePost([Body] Post post);

        [Put("/posts/{id}")]
        Task<Post> UpdatePost(int id, [Body] Post post);

        [Delete("/posts/{id}")]
        Task DeletePost(int id);
    }
}

