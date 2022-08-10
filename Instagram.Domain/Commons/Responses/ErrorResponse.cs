namespace Instagram.Domain.Commons.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public short Code { get; set; }

        public ErrorResponse(string message, short code)
        {
            Message = message;

            Code = code;
        }
    }
}