namespace Domain.Exceptions;

public class ApiToReturn
{
    public ApiToReturn()
    {
        
    }

   

    public ApiToReturn(string message)
    {
        Message = message;
        Messages.Add(message);
    }

    public ApiToReturn(int statusCode, string message)
    {
        statuscode = statusCode;
        Message = message;
        Messages.Add(message);
    }

    
    public ApiToReturn(int statuscode, List<string> messages)
    {
        this.statuscode = statuscode;
       
        Messages = messages;
    }
    public ApiToReturn(int statuscode,  List<string> messages, string details)
    {
        this.statuscode = statuscode;
      
        Messages = messages;
        Detail = details;
    }
    public ApiToReturn(int statuscode, string message, string details)
    {
        this.statuscode = statuscode;

        Message = message;
        Messages.Add(message);
        Detail = details;
    }
    public ApiToReturn(int statuscode, string message,List<string> messages,string details)
    {
        this.statuscode = statuscode;

        Message = message;
        Messages.Add(message);
        Detail = details;
    }
    public string Message { get; set; }
    public int  statuscode { get; set; }
    public string Detail { get; set; }
    public List<string> Messages { get; set; } = new();

}