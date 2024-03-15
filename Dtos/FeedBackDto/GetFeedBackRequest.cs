namespace Feedback.Dtos.FeedBackDto
{
    public class GetFeedBackRequest : Pagination
    {
        public long UserId { get; set; }
        public long ShopId { get; set; }
    }
}
