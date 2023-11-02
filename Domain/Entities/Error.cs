namespace Domain.Entities
{
    public class Error
    {
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; protected set; }
        public string Message { get; protected set; }
    }
}