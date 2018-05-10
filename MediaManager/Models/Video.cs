using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.Models
{
    [Table("Video")]
    public class Video
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("FilePath")]
        public string FilePath { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
