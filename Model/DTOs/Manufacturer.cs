﻿using System;
using System.Collections.Generic;

namespace NewTrade.Model.DTOs
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        public int IdManufacturer { get; set; }
        public string ManufacturerName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
