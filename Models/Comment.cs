using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace NotesImageSharingApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Post Post { get; set; }
    }
}