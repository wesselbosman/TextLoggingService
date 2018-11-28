using System;

namespace TextLoggingService.Core.Logging
{
    public class LogModel
    {
        public long Id { get; }
        public DateTime Date { get; }
        public string Message { get; }

        public LogModel(long id, DateTime date, string message)
        {
            Id = id;
            Date = date;
            Message = message;
        }
    }
}