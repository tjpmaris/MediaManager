using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    public enum Role { User, Admin }

    [Table("User")]
    public class User
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Role")]
        public Role Role { get; set; }

        public List<Picture> Pictures { get; set; }

        public List<Song> Songs { get; set; }

        public List<Video> Videos { get; set; }
    }
}
