using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    [Table("Picture")]
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [JsonIgnore]
        [Column("FilePath")]
        public string FilePath { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
