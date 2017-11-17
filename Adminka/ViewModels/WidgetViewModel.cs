using Blog.Core.Models;
using Blog.Core.Stores;
using System.Collections.Generic;

namespace Adminka.ViewModels
{
    public class WidgetViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Post> LatestPosts { get; set; }
    }
}