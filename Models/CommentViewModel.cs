// to handle comment data.
namespace NotesImageSharingApp.Models
{
    public class CommentViewModel
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}