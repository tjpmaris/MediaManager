using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    public enum Role { User, Admin }

    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [JsonIgnore]
        [Column("Password")]
        public string Password { get; set; }

        [Column("Role")]
        public Role Role { get; set; }

        [JsonIgnore]
        public List<Picture> Pictures { get; set; }

        [JsonIgnore]
        public List<Song> Songs { get; set; }

        [JsonIgnore]
        public List<Video> Videos { get; set; }
    }
}
