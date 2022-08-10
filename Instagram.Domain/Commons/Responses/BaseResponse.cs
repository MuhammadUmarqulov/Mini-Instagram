namespace Instagram.Domain.Commons.Responses
{
    public class BaseResponse<TEntity>
    {
        public TEntity Data { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
