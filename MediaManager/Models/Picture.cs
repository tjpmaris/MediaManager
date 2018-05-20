using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    [Table("Picture")]
    public class Picture : FileModel
    {
        [Column("UserId")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
