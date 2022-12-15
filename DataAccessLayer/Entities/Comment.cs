namespace DataAccessLayer.Entities
{
    public class Comment : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ContentText { get; set; } = null!;
        public string CreateDate { get; set; } = null!;
    }
}
