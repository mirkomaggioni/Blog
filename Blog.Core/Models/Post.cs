using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Core.Models
{
    [Table("Posts")]
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<PostCategory> PostCategories { get; set; }
        public List<PostTag> PostTags { get; set; }
        public bool Published { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyUser { get; set; }
    }
}
