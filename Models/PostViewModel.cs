//This view model is used to pass data to the view, including form data and related entities.
namespace NotesImageSharingApp.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; } // Added PostId property
        public Note Note { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}