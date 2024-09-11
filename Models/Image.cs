//image.cs model
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace NotesImageSharingApp.Models
{
    public class Image
    {
        public int ImageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Foreign key to the user
        public string UserId { get; set; }
    }
}