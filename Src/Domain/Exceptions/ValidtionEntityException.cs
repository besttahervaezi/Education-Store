namespace Domain.Exceptions;

public class ValidtionEntityException:BaseException
{
    public ValidtionEntityException(List<string> messages) : base(messages)
    {
    }

    public ValidtionEntityException(string message) : base(message)
    {
    }

    public ValidtionEntityException():base("خطایی رخ داده است لطفامجددا تلاش کنید")
    {
        
    }
}