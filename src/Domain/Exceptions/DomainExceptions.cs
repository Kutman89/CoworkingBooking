namespace Domain.Exceptions;

public class DomainExceptions : Exception
{
    public DomainExceptions(string message, string paramName) : base(message)
    {
    }
}
