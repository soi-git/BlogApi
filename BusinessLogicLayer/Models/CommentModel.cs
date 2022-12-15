namespace BusinessLogicLayer.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ContentText { get; set; } = null!;
        public string CreateDate { get; set; } = null!;
    }
}
