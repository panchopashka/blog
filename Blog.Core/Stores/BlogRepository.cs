using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Core.EntityFramework;
using System.Linq;
using System.Data.Entity;
using System;

namespace Blog.Core.Stores
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _dbContext;

        public BlogRepository(BlogDbContext context)
        {
            _dbContext = context;
        }

        public Task<Category> CategoryAsync(string categorySlug)
        {
            return _dbContext.Categories.FirstOrDefaultAsync(t => t.UrlSlug.Equals(categorySlug));
        }

        public List<Post> Posts(int pageNo, int pageSize)
        {
            return _dbContext.Posts
                              .Where(p => p.Published)
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Include(p => p.Category)
                              .Include(p => p.Tags)
                              .ToList();
        }

        public Task<List<Post>> PostsAsync(int pageNo, int pageSize)
        {
            return _dbContext.Posts
                              .Where(p => p.Published)
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Include(p => p.Category)
                              .Include(p => p.Tags)
                              .ToListAsync();
        }

        public List<Post> Posts(int pageNo, int pageSize, string sortColumn,
                            bool sortByAscending)
        {
            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                    {
                        return _dbContext.Posts
                                        .OrderBy(p => p.Title)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .Include(p=>p.Tags)
                                        .ToList();
                    }
                    else
                    {
                        return _dbContext.Posts
                                       .OrderByDescending(p => p.Title)
                                       .Skip(pageNo * pageSize)
                                       .Take(pageSize)
                                       .Include(p => p.Category)
                                       .Include(p => p.Tags)
                                       .ToList();
                    }
                    break;
                case "Published":
                    if (sortByAscending)
                    {
                        return _dbContext.Posts
                                         .OrderBy(p => p.Published)
                                         .Skip(pageNo * pageSize)
                                         .Take(pageSize)
                                         .Include(p => p.Category)
                                         .Include(p => p.Tags)
                                         .ToList();
                    }
                    else
                    {
                        return _dbContext.Posts
                                        .OrderByDescending(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .Include(p => p.Tags)
                                        .ToList();
                    }
                    break;
                case "PostedOn":
                    if (sortByAscending)
                    {
                        return _dbContext.Posts
                                      .OrderBy(p => p.PostedOn)
                                      .Skip(pageNo * pageSize)
                                      .Take(pageSize)
                                      .Include(p => p.Category)
                                      .Include(p => p.Tags)
                                      .ToList();
                    }
                    else
                    {
                        return _dbContext.Posts
                                      .OrderByDescending(p => p.PostedOn)
                                      .Skip(pageNo * pageSize)
                                      .Take(pageSize)
                                      .Include(p => p.Category)
                                      .Include(p => p.Tags)
                                      .ToList();
                    }
                    break;
                case "Modified":
                    if (sortByAscending)
                    {
                        return _dbContext.Posts
                                    .OrderBy(p => p.Modified)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.Category)
                                    .Include(p => p.Tags)
                                    .ToList();
                    }
                    else
                    {
                        return _dbContext.Posts
                                    .OrderByDescending(p => p.Modified)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.Category)
                                    .Include(p => p.Tags)
                                    .ToList();
                    }
                    break;
                case "Category":
                    if (sortByAscending)
                    {
                        return _dbContext.Posts
                                    .OrderBy(p => p.Category)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.Category)
                                    .Include(p => p.Tags)
                                    .ToList();
                    }
                    else
                    {
                        return _dbContext.Posts
                                    .OrderByDescending(p => p.Category)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.Category)
                                    .Include(p => p.Tags)
                                    .ToList();
                    }             
                default:
                    return _dbContext.Posts
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Include(p => p.Category)
                                .Include(p => p.Tags)
                                .ToList();
            }
        }

        public Task<List<Post>> PostsForCategoryAsync(string categorySlug, int pageNo, int pageSize)
        {
            return _dbContext.Posts
                      .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                      .OrderByDescending(p => p.PostedOn)
                      .Skip(pageNo * pageSize)
                      .Take(pageSize)
                      .Include(p => p.Category)
                      .Include(p => p.Tags)
                      .ToListAsync();
        }

        public Task<int> TotalPostsAsync()
        {
            return _dbContext.Posts.Where(p => p.Published).CountAsync();
        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return _dbContext.Posts
                    .Where(p => !checkIsPublished || p.Published == true)
                    .Count();
        }

        public Task<int> AddPostAsync(Post post)
        {
            if (post.Category != null)
                post.Category = _dbContext.Categories.FirstOrDefault(c=>c.Id == post.Category.Id);

            post.PostedOn = DateTime.UtcNow;

            _dbContext.Posts.Add(post);
            return _dbContext.SaveChangesAsync();
        }

        public async Task EditPostAsync(Post post)
        {
            if (post.Category != null)
                post.Category = _dbContext.Categories.FirstOrDefault(c => c.Id == post.Category.Id);

            var editPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            _dbContext.Posts.Remove(editPost);

            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var delPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            _dbContext.Posts.Remove(delPost);

            await _dbContext.SaveChangesAsync();
        }

        public Task<int> TotalPostsForCategoryAsync(string categorySlug)
        {
            return _dbContext.Posts
            .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
            .CountAsync();
        }

        public Task<List<Post>> PostsForTagAsync(string tagSlug, int pageNo, int pageSize)
        {
            return _dbContext.Posts
                              .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Include(p => p.Category)
                              .Include(p => p.Tags)
                              .ToListAsync();
        }

        public Task<int> TotalPostsForTagAsync(string tagSlug)
        {
            return _dbContext.Posts
                        .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                        .CountAsync();
        }

        public Task<Tag> TagAsync(string tagSlug)
        {
            return _dbContext.Tags.FirstOrDefaultAsync(t => t.UrlSlug.Equals(tagSlug));
        }

        public Task<List<Post>> PostsForSearchAsync(string search, int pageNo, int pageSize)
        {
            return _dbContext.Posts
                                .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Include(p => p.Category)
                                .Include(p=>p.Tags)
                                .ToListAsync();
        }

        public Task<int> TotalPostsForSearchAsync(string search)
        {
            return _dbContext.Posts
                    .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                    .CountAsync();
        }

        public Task<Post> PostAsync(int year, int month, string titleSlug)
        {
            return _dbContext.Posts
                            .Include(p => p.Category)
                            .Include(p => p.Tags)
                            .FirstOrDefaultAsync(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));
        }

        public List<Category> Categories()
        {
            return _dbContext.Categories.OrderBy(p => p.Name).ToList();
        }

        public Task<List<Category>> CategoriesAsync()
        {
            return _dbContext.Categories.ToListAsync();
        }

        public List<Tag> Tags()
        {
            return _dbContext.Tags.OrderBy(p => p.Name).ToList();
        }

        public Task<List<Tag>> TagsAsync()
        {
            return _dbContext.Tags.ToListAsync();
        }

        public Category Category(int id)
        {
            return _dbContext.Categories.FirstOrDefault(t => t.Id == id);
        }

        public Tag Tag(int id)
        {
            return _dbContext.Tags.FirstOrDefault(t => t.Id == id);
        }
    }
}
