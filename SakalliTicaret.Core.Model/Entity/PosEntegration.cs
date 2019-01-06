﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class PosEntegration : EntityBase
    {
        public string StoreCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Installments { get; set; }
    }
}
