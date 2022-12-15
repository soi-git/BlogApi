namespace BusinessLogicLayer.Models
{
    public class LikeModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CreateDate { get; set; } = null!;
    }
}
