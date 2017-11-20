using Blog.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Stores
{
    public interface IBlogRepository
    {
        List<Post> Posts(int pageNo, int pageSize);
        Task<List<Post>> PostsAsync(int pageNo, int pageSize);
        List<Post> Posts(int pageNo, int pageSize, string sortColumn,
                            bool sortByAscending);

        Task<int> TotalPostsAsync();
        int TotalPosts(bool checkIsPublished = true);

        Task<int> AddPostAsync(Post post);
        Task EditPostAsync(Post post);
        Task DeletePostAsync(int id);

        Task<int> AddCategoryAsync(Category category);
        Task EditCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);

        Task<int> AddTagAsync(Tag tag);
        Task EditTagAsync(Tag tag);
        Task DeleteTagAsync(int id);

        Task<List<Post>> PostsForCategoryAsync(string categorySlug, int pageNo, int pageSize);
        Task<int> TotalPostsForCategoryAsync(string categorySlug);
        Task<Category> CategoryAsync(string categorySlug);
        Task<List<Post>> PostsForTagAsync(string tagSlug, int pageNo, int pageSize);
        Task<int> TotalPostsForTagAsync(string tagSlug);
        Task<Tag> TagAsync(string tagSlug);
        Task<List<Post>> PostsForSearchAsync(string search, int pageNo, int pageSize);
        Task<int> TotalPostsForSearchAsync(string search);

        Task<Post> PostAsync(int year, int month, string titleSlug);

        Task<List<Category>> CategoriesAsync();
        List<Category> Categories();

        List<Tag> Tags();
        Task<List<Tag>> TagsAsync();

        Category Category(int id);

        Tag Tag(int id);
    }
}
