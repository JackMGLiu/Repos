using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("UserTwo")]
    public class UserTwo
    {
        [Dapper.Contrib.Extensions.Key]
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserPass { get; set; }

        public string RealName { get; set; }

        public string OpenKey { get; set; }
    }
}
