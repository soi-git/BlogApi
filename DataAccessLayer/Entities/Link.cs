namespace DataAccessLayer.Entities
{
    public class Link : BaseEntity
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
