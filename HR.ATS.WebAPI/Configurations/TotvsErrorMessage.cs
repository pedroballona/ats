namespace HR.ATS.WebAPI.Configurations
{
    internal class TotvsErrorMessage
    {
        public string Code { get; }
        public string Message { get; }
        public string DetailedMessage { get; }

        public TotvsErrorMessage(string code, string message, string detailedMessage)
        {
            Code = code;
            Message = message;
            DetailedMessage = detailedMessage;
        }
    }
}