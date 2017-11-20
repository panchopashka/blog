using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Models
{
    public class Category
    {
        [Key]
        public virtual int Id
        { get; set; }

        [Required(ErrorMessage = "Name: Field is required")]
        [MaxLength(50, ErrorMessage = "Name: Length should not exceed 50 characters")]
        public virtual string Name
        { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "UrlSlug: Length should not exceed 50 characters")]
        public virtual string UrlSlug
        { get; set; }

        [MaxLength(200)]
        public virtual string Description
        { get; set; }

        [JsonIgnore]
        public virtual IList<Post> Posts
        { get; set; }
    }
}
