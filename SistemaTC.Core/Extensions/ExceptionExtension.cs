namespace SistemaTC.Core.Extensions;
public static class ExceptionExtension
{
    public static string GetLastException(this Exception exception)
    {
        var lastException = exception;
        while (lastException.InnerException != null)
        {
            lastException = lastException.InnerException;
        }

        return lastException.Message;
    }
}
