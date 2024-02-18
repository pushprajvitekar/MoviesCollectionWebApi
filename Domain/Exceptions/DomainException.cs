namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainErrorCode ErrorCodeEnum { get; protected set; }
        public int ErrorCode => (int)ErrorCodeEnum;
        public DomainException(string? message, Exception? innerException, DomainErrorCode errorCode) : base(message, innerException)
        {
            ErrorCodeEnum = errorCode;
        }
    }
}
