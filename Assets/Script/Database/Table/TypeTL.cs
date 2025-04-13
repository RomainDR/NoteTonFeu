using Postgrest.Attributes;
using Postgrest.Models;

namespace Script.Database.Table
{
    [Table("TypeTL")]
    public class TypeTL : BaseModel
    {
        [PrimaryKey("id_type")] public int Id { get; set; }
        [Column] public string name { get; set; }
    }
}