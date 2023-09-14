namespace SecurityHelper
{
    internal interface ILogger
    {
        void LogInformation(string message, params object[] arguments);
    }
}
