//comment.cs model
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
        [Key]
        public int CommentId { get; set; }

      
        public string? Content { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        // Foreign key to the user
        public string? UserId { get; set; }
        // Foreign key to the note or image
        public int? NoteId { get; set; }
        public int? ImageId { get; set; }
    }
}
