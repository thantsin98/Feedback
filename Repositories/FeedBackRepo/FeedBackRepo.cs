using Azure.Core;
using Feedback.Data;
using Feedback.Dtos;
using Feedback.Dtos.FeedBackDto;
using Feedback.Dtos.Status;
using Feedback.Interfaces;
using Feedback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Repositorys.FeedBackRepo
{
    public class FeedBackRepo : IFeedBack
    {
        private readonly ApplicationDbContext _context;
        public FeedBackRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetFeedBackResponse> GetAll([FromQuery] Pagination pagination)
        {
            try
            {
                var feedBack = await _context.FeedBack
                    .Where(f => f.IsActive == true)
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .ToListAsync();
                if (feedBack == null || feedBack.Count == 0)
                {
                    return new GetFeedBackResponse
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        StatusMessage = "Your's feedback is null"
                    };
                }
                return new GetFeedBackResponse
                {
                    FeedBacks = feedBack,
                    PageItems = feedBack.Count,
                    PageNumbers = pagination.PageNumber,
                    StatusCodes = StatusCodes.Status200OK,
                    StatusMessage = "OK"
                };
            }
            catch (Exception ex)
            {
                return new GetFeedBackResponse
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        public async Task<GetFeedBackResponse> GetByUser([FromQuery] GetFeedBackRequest getFeedBackRequest)
        {
            try
            {
                var feedBack = await _context.FeedBack
                    .Where(f => f.IsActive == true
                    && f.UserId == getFeedBackRequest.UserId 
                    && f.ShopId == getFeedBackRequest.ShopId)
                    .Skip((getFeedBackRequest.PageNumber-1)*getFeedBackRequest.PageSize)
                    .Take(getFeedBackRequest.PageSize)
                    .ToListAsync();
                if(feedBack == null || feedBack.Count == 0)
                {
                    return new GetFeedBackResponse
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        StatusMessage = "Your's feedback is null"
                    };
                }
                return new GetFeedBackResponse
                {
                    FeedBacks = feedBack,
                    PageItems = feedBack.Count,
                    PageNumbers = getFeedBackRequest.PageNumber,
                    StatusCodes = StatusCodes.Status200OK,
                    StatusMessage = "OK"
                };
            }catch(Exception ex)
            {
                return new GetFeedBackResponse
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }

        public async Task<GetFeedBackResponse> GetOther([FromQuery] GetFeedBackRequest getFeedBackRequest)
        {
            try
            {
                var feedBack = await _context.FeedBack
                    .Where(f => f.IsActive.Equals(true)
                    && f.UserId != getFeedBackRequest.UserId
                    && f.ShopId == getFeedBackRequest.ShopId)
                    .Skip((getFeedBackRequest.PageNumber-1)*getFeedBackRequest.PageSize)
                    .Take(getFeedBackRequest.PageSize)
                    .ToListAsync();
                if (feedBack == null || feedBack.Count == 0)
                {
                    return new GetFeedBackResponse
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        StatusMessage = "Feedback is null"
                    };
                }
                return new GetFeedBackResponse
                {
                    FeedBacks = feedBack,
                    PageItems = feedBack.Count,
                    PageNumbers = getFeedBackRequest.PageNumber,
                    StatusCodes = StatusCodes.Status200OK,
                    StatusMessage = "More Reviews"
                };
            }
            catch (Exception ex)
            {
                return new GetFeedBackResponse
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    StatusMessage = ex.GetBaseException().Message
                };
            }
        }
        
        public async Task<BaseResponse> Add(AddFeedBackRequest addFeedBack)
        {
            try
            {
                if (addFeedBack.Description == null || addFeedBack.Rating == 0
                    || addFeedBack.Description == "")
                {
                    return new BaseResponse
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        StatusMessage = "Description or Rating are empty."
                    };
                }
                var feedback = new FeedBack
                {
                    Description = addFeedBack.Description,
                    Rating = addFeedBack.Rating,
                    ShopId = addFeedBack.ShopId,
                    UserId = addFeedBack.UserId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = addFeedBack.UserId,
                    IsActive = true
                };
                _context.FeedBack.Add(feedback);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    StatusCodes = StatusCodes.Status201Created,
                    StatusMessage = "Feedback Added."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse { StatusMessage = ex.GetBaseException().Message };
            }
        }

        public async Task<BaseResponse> Edit(long userId,long feedBackId, EditFeedBackRequest request)
        {
            try
            {
                var feedBack = await _context.FeedBack.Where(f => f.IsActive == true 
                && f.Id == feedBackId && f.UserId == userId).SingleOrDefaultAsync();
                if (feedBack == null)
                {
                    return new BaseResponse
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        StatusMessage = "Not found."
                    };
                }
                if (request.Description == "" || request.Description == null || request.Rating == 0)
                {
                    return new BaseResponse
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        StatusMessage = "Description or Rating are empty."
                    };
                }
                feedBack.Description = request.Description;
                feedBack.Rating = request.Rating;
                feedBack.UpdatedDate = DateTime.Now;
                feedBack.UpdateBy = userId;
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    StatusCodes = StatusCodes.Status200OK,
                    StatusMessage = "Updated Successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse { StatusMessage = ex.Message };
            }
        }

        public async Task<BaseResponse> Delete(long userId, long feedBackId)
        {
            try
            {
                var feedBack = await _context.FeedBack.Where(f => f.IsActive == true
                && f.Id == feedBackId && f.UserId == userId).SingleOrDefaultAsync();
                if (feedBack == null)
                {
                    return new BaseResponse
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        StatusMessage = "Not found."
                    };
                }
                feedBack.IsActive = false;
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    StatusCodes = StatusCodes.Status200OK,
                    StatusMessage = "Deleted Successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse { StatusMessage = ex.Message };
            }
        }
    }
}
