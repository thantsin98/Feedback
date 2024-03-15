using Feedback.Dtos;
using Feedback.Dtos.FeedBackDto;
using Feedback.Dtos.Status;

namespace Feedback.Interfaces
{
    public interface IFeedBack
    {
        public Task<GetFeedBackResponse> GetAll(Pagination pagination);
        public Task<GetFeedBackResponse> GetByUser(GetFeedBackRequest request);
        public Task<GetFeedBackResponse> GetOther(GetFeedBackRequest request);
        public Task<BaseResponse> Add(AddFeedBackRequest request);
        public Task<BaseResponse> Edit(long feedBackId, long userId, EditFeedBackRequest request);
        public Task<BaseResponse> Delete(long feedBackId, long userId);
    }
}
