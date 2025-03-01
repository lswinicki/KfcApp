namespace Domain.Models;

[Serializable]
public class ProblemException : Exception
{
    public string Error { get; }
    public string Message { get; }

    public ProblemException(string error, string message) : base(message)
    {
        Error = error;
        Message = message;
    }
}

[Serializable]
public class ProblemValidationException : Exception
{
    public string Error { get; }
    public string Message { get; }

    public ProblemValidationException(string error,string message)
    {
        Error = error;
        Message = message;
    }
}