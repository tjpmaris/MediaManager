using MediaManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaManager.ApiModels
{
    public class FlatUser
    {
        public FlatUser(User user)
        {
            this.Id = user.Id;
            this.Role = user.Role;
            this.Username = user.Username;
        }

        public int? Id { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }
    }
}
