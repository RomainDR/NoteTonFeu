using Postgrest.Attributes;
using Postgrest.Models;

namespace Script.Database.Table
{
    [Table("TrafficLight")]
    public class TrafficLight : BaseModel
    {
        [PrimaryKey("id_traffic")] public int Id { get; set; }
        [Column] public string latitude { get; set; }
        [Column] public int longitude { get; set; }
        [Column] public int type { get; set; }
        [Column] public int id_type { get; set; }
    }
}