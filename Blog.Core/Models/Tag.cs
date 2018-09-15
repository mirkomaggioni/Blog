using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Core.Models
{
    [Table("Tags")]
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
