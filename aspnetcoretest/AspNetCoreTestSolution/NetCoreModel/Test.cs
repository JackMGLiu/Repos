using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("test")]
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}