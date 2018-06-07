using System;

namespace MediaManager.Models
{
    public class RavenModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public void Update(RavenModel model)
        {
            this.Name = model.Name;
        }
    }
}
