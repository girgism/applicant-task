namespace Applicants.APIs;

public class BaseResponse
{
    public int statusCode { get; set; }
    public string errorMessage { get; set; }
    public object additionalData { get; set; }

    public static BaseResponse CreateProblemDetail(string ErrorMessage)
    {
        return new BaseResponse
        {
            statusCode = StatusCodes.Status400BadRequest,
            errorMessage = ErrorMessage
        };
    }


    public static BaseResponse CreateProblemDetail(string ErrorMessage, object additioanlData)
    {
        return new BaseResponse
        {
            statusCode = StatusCodes.Status400BadRequest,
            errorMessage = ErrorMessage,
            additionalData = additioanlData
        };
    }
}
