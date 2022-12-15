namespace DataAccessLayer.Entities
{
    public class Like : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CreateDate { get; set; } = null!;
    }
}
