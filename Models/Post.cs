namespace NotesImageSharingApp.Models
{
    public class Post
    {
        public int PostId { get; set; }
        
        // Optional image URL
        public string? ImageUrl { get; set; }

        // Post content or note
        public string Content { get; set; }

        // Date when the post was created
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Foreign key to the user who created the post
        public string UserId { get; set; }

        // Relationship with comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}