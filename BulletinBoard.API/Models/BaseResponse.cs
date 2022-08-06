using BulletinBoard.API.Models.Enum;

namespace BulletinBoard.API.Models
{
    public class BaseResponse
    {
        public BaseResponse() { }

        public BaseResponse(int status, string message)
        {
            Status = status;
            Message = message;
        }
        public BaseResponse(int status, string message, EnumResponse responseCode)
        {
            Status = status;
            Message = message;
            ResponseCode = responseCode;
        }
        public BaseResponse(int status)
        {
            Status = status;
            Message = status == 0
                ? "Запрос успешно выполнен."
                : "Ошибка при выполнении запроса.";
        }
        public int Status { get; set; }

        public string Message { get; set; }

        public bool IsSuccess => Status == 0;

        public EnumResponse ResponseCode { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse() { }

        public BaseResponse(int status, string message) : base(status, message) { }

        public BaseResponse(int status) : base(status) { }
        public BaseResponse(int status, string message, EnumResponse responseCode) : base(status, message, responseCode) { }

        public BaseResponse(T value)
        {
            Status = 0;
            Message = "Запрос успешно выполнен.";
            Value = value;
        }

        public BaseResponse(int status, T value) : base(status)
        {
            Value = value;
        }

        public BaseResponse(int status, string message, T value) : base(status)
        {
            Message = message;
            Value = value;
        }
        public T Value { get; set; }
    }
}
