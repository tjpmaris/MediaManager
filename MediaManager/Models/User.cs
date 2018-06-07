using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("Email")]
        public string Email { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [JsonIgnore]
        [Column("Password")]
        public string Password { get; set; }

        [Column("Role")]
        public string Role { get; set; }

        [JsonIgnore]
        [Column("AuthToken")]
        public string AuthToken { get; set; }

        [Column("Pictures")]
        public string Pictures { get; set; }

        [Column("Songs")]
        public string Songs { get; set; }

        [Column("Videos")]
        public string Videos { get; set; }
    }
}
