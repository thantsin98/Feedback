using Feedback.Dtos;
using Feedback.Dtos.FeedBackDto;
using Feedback.Dtos.Status;
using Feedback.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBack _repo;
        public FeedBackController(IFeedBack repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<GetFeedBackResponse> GetAll([FromQuery] Pagination pagination)
        {
            try
            {
                return await _repo.GetAll(pagination);
            }catch (Exception ex)
            {
                return new GetFeedBackResponse
                {
                    StatusCodes = 500,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        [HttpGet("Get")]
        public async Task<GetFeedBackResponse> GetByUser([FromQuery] GetFeedBackRequest request)
        {
            try
            {
                return await _repo.GetByUser(request);
            }
            catch (Exception ex)
            {
                return new GetFeedBackResponse
                {
                    StatusCodes = 500,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        [HttpGet("GetOther")]
        public async Task<GetFeedBackResponse> GetOther([FromQuery] GetFeedBackRequest request)
        {
            try
            {
                return await _repo.GetOther(request);
            }
            catch (Exception ex)
            {
                return new GetFeedBackResponse
                {
                    StatusCodes = 500,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        [HttpPost("Add")]
        public async Task<BaseResponse> Add(AddFeedBackRequest addFeedBack)
        {
            try
            {
                return await _repo.Add(addFeedBack);
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    StatusCodes = 500,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        [HttpPut("Edit")]
        public async Task<BaseResponse> Edit(long userId, long feedBackId, EditFeedBackRequest request)
        {
            try
            {
                return await _repo.Edit(userId, feedBackId, request);
            }
            catch (Exception ex)
            {
                return new BaseResponse 
                { 
                    StatusCodes = 500,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        [HttpDelete("Delete")]
        public async Task<BaseResponse> Delete(long userId,long feedBackId)
        {
            try
            {
                return await _repo.Delete(userId, feedBackId);
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    StatusCodes = 500,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }
    }
}
