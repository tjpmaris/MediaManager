using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MediaManager.Models
{
    public class FileModel : DBModel
    {
        [JsonIgnore]
        [Column("FilePath")]
        public string FilePath { get; set; }

        public void Update(FileModel obj)
        {
            base.Update(obj);
            FilePath = obj.FilePath;
        }
    }
}
