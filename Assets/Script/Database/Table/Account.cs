using Postgrest.Attributes;
using Postgrest.Models;

namespace Script.Database.Table
{
    [Table("account")]
    public class Account : BaseModel
    {
        [PrimaryKey("id_account")] public int Id { get; set; }
        [Column] public string email { get; set; }
        [Column] public string password { get; set; }
        [Column] public string name { get; set; }
        [Column] public bool active { get; set; }
        [Column("id_role")] public int idRole { get; set; }

        public Account(string email, string password, string name, bool active = true, int idRole = 0)
        {
            this.email = email;
            this.password = password;
            this.name = name;
            this.active = active;
            this.idRole = idRole;
        }

        public Account() { }
    }
}