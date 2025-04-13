using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Script.Database.Table
{
    [Table("notation")]
    public class Notation : BaseModel
    {
        [PrimaryKey("id_notation")] public int Id { get; set; }
        [Column] public int note { get; set; }
        [Column] public string text_avis { get; set; }
        [Column("date")] public DateTime date { get; set; }
        [Column("id_account")] public int idAccount { get; set; }
        [Column("id_traffic")] public int idTraffic { get; set; }
    }
}