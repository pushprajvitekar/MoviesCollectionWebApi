namespace Domain.Exceptions
{
    public class InfrastructureException : DomainException
    {
        public InfrastructureException(string? message, Exception? innerException) : base(message, innerException, DomainErrorCode.InfrastructureError)
        {
        }
    }
}
