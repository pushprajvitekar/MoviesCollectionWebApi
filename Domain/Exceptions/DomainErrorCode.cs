namespace Domain.Exceptions
{
    public enum DomainErrorCode
    {
        Exists=400,
        NotFound=404,
        Conflict= 401,
        InfrastructureError=551
    }
}
