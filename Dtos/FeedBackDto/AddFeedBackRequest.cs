namespace Feedback.Dtos.FeedBackDto
{
    public class AddFeedBackRequest
    {
        public string Description { get; set; }
        public float Rating { get; set; }
        public long UserId { get; set; }
        public long ShopId { get; set; }
    }
}
