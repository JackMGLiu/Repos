using System;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("Logs")]
    public class Logs
    {
        [Key]
        public int LogId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LogLevel { get; set; }
        public string Origin { get; set; }
        public string Action { get; set; }
        public string Request { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
    }
}