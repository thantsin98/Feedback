namespace Feedback.Models
{
    public class FeedBack
    {
        public long Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public float Rating { get; set; }
        public long UserId { get; set; }
        public long ShopId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdateBy { get; set; }
        public bool IsActive { get; set; } 
    }
}
