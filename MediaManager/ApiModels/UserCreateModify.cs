﻿using MediaManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaManager.ApiModels
{
    public class UserCreateModify
    {
        public int? Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
