namespace Feedback.Dtos.Status;

public class BaseResponse
{
    public int StatusCodes { get; set; }
    public string StatusMessage { get; set; } = string.Empty;
}
