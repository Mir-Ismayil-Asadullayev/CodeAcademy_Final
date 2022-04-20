﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public byte DiscountPercent { get; set; }
        public List<Product> Products { get; set; }
    }
}
