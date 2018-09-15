using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Core.Models
{
    [Table("Replies")]
    public class Reply
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public bool Published { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyUser { get; set; }
    }
}
