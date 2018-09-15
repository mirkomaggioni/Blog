using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Core.Models
{
    [Table("PostCategories")]
    public class PostCategory
    {
        public Guid Id { get; set; }
        public Post Post { get; set; }
        public Category Category { get; set; }
    }
}
