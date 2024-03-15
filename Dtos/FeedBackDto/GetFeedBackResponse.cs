using Feedback.Dtos.Status;
using Feedback.Models;

namespace Feedback.Dtos.FeedBackDto
{
    public class GetFeedBackResponse : BaseResponse
    {
        public List<FeedBack> FeedBacks { get; set; }
        public int PageItems { get; set; }
        public int PageNumbers { get; set; }
    }
}
