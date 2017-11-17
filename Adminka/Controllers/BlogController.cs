using Adminka.ViewModels;
using Blog.Core.Stores;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Adminka.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ViewResult> Posts(int p = 1)
        {
            var posts = await _blogRepository.PostsAsync(p - 1, 10);

            var totalPosts = await _blogRepository.TotalPostsAsync();

            var listViewModel = new ListViewModel
            {
                Posts = posts,
                TotalPosts = totalPosts
            };

            ViewBag.Title = "Latest Posts";

            return View("List", listViewModel);
        }

        public async Task<ViewResult> Category(string categorySlug, int p = 1)
        {
            var category = await _blogRepository.CategoryAsync(categorySlug);

            if (category == null)
                throw new HttpException(404, "Category not found");

            var posts = await _blogRepository.PostsForCategoryAsync(categorySlug, p - 1, 10);
            var totalPosts = await _blogRepository.TotalPostsForCategoryAsync(categorySlug);

            var viewModel = new ListViewModel()
            {
                Posts = posts,
                TotalPosts = totalPosts,
                Category = category
            };

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                        viewModel.Category.Name);

            return View("List", viewModel);
        }

        public async Task<ViewResult> Tag(string tagSlug, int p = 1)
        {
            var tag = await _blogRepository.TagAsync(tagSlug);

            if (tag == null)
                throw new HttpException(404, "Tag not found");

            var posts = await _blogRepository.PostsForTagAsync(tagSlug, p - 1, 10);
            var totalPosts = await _blogRepository.TotalPostsForTagAsync(tagSlug);

            var viewModel = new ListViewModel()
            {
                Posts = posts,
                TotalPosts = totalPosts,
                Tag = tag
            };

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""",
                    viewModel.Tag.Name);
            return View("List", viewModel);
        }

        public async Task<ViewResult> Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found
                        for search text ""{0}""", s);

            var posts = await _blogRepository.PostsForSearchAsync(s, p - 1, 10);
            var totalPosts = await _blogRepository.TotalPostsForSearchAsync(s);


            var viewModel = new ListViewModel()
            {
                Posts = posts,
                TotalPosts = totalPosts,
                SearchText = s
            };
            return View("List", viewModel);
        }

        public async Task<ViewResult> Post(int year, int month, string title)
        {
            var post = await _blogRepository.PostAsync(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false)
                throw new HttpException(401, "The post is not published");

            return View(post);
        }

        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var categories = _blogRepository.Categories();
            var tags = _blogRepository.Tags();
            var latestPosts = _blogRepository.Posts(0, 10);

            var widgetViewModel = new WidgetViewModel
            {
                Categories = categories,
                Tags = tags,
                LatestPosts = latestPosts
            };
            return PartialView("_Sidebars", widgetViewModel);
        }
    }
}