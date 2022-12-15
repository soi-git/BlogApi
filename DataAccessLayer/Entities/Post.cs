namespace DataAccessLayer.Entities
{
    public  class Post : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Summary { get; set; }
        public string? ContentText { get; set; }
        public string CreateDate { get; set; } = null!;
        public string? LastUpdateDate { get; set; }
        public int UserId { get; set; }
    }
}
