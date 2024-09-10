//This model represents the data structure stored in the database.
namespace NotesImageSharingApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}