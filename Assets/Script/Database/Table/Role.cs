using Postgrest.Attributes;
using Postgrest.Models;

namespace Script.Database.Table
{
    [Table("Role")]
    public class Role : BaseModel
    {
        [PrimaryKey("id_role")] public int Id { get; set; }
        [Column] public string name { get; set; }
        [Column] public int power { get; set; }
    }
}