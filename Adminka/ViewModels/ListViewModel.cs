using Blog.Core.Models;
using System.Collections.Generic;

namespace Adminka.ViewModels
{
    public class ListViewModel
    {
        public List<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }
        public string SearchText { get; set; }
    }
}