using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Models
{
    public class Tag
    {
        [Key]
        public virtual int Id
        { get; set; }

        [Required(ErrorMessage = "Name: Field is required")]
        [MaxLength(50)]
        public virtual string Name
        { get; set; }

        [Required(ErrorMessage = "UrlSlug: Field is required")]
        [MaxLength(50)]
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
