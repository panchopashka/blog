using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Models
{
    public class Post
    {
        [Key]
        public virtual int Id
        { get; set; }

        [Required(ErrorMessage = "Title: Field is required")]
        [MaxLength(5000)]
        public virtual string Title
        { get; set; }

        [Required(ErrorMessage = "ShortDescription: Field is required")]
        [MaxLength(5000)]
        public virtual string ShortDescription
        { get; set; }

        [Required(ErrorMessage = "Description: Field is required")]
        [MaxLength(5000)]
        public virtual string Description
        { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Meta: Length should not exceed 1000 characters")]
        public virtual string Meta
        { get; set; }

        [Required(ErrorMessage = "Meta: Field is required")]
        [MaxLength(200, ErrorMessage = "Meta: UrlSlug should not exceed 200 characters")]
        public virtual string UrlSlug
        { get; set; }

        [Required]
        public virtual bool Published
        { get; set; }

        [Required(ErrorMessage = "PostedOn: Field is required")]
        public virtual DateTime PostedOn
        { get; set; }

        public virtual DateTime? Modified
        { get; set; }

        [Required]
        public virtual Category Category
        { get; set; }

        public virtual IList<Tag> Tags
        { get; set; }
    }
}
