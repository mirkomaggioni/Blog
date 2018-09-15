using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Core.Models
{
    [Table("PostTags")]
    public class PostTag
    {
        public Guid Id { get; set; }
        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
