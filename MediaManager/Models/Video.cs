using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    [Table("Video")]
    public class Video : FileModel
    {
        [Column("UserId")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
