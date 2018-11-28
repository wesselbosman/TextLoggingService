using System;
using System.ComponentModel.DataAnnotations;

namespace TextLoggingService.Core.Logging
{
    public class LogModel
    {
        public long Id { get; }
        public DateTime Date { get; }

        [MaxLength(255)]
        public string Message { get; }

        public LogModel(long id, DateTime date, string message)
        {
            Id = id;
            Date = date;
            Message = message;
        }
    }
}