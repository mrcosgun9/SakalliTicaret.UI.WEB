﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class InsertedUsers:EntityBase
    {
        public string Actions { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
