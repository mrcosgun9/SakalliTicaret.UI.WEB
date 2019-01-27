﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class ProductProperty:EntityBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int PropertyPropertyValuesId { get; set; }
        public PropertyPropertyValues PropertyPropertyValues { get; set; }
    }
}
