﻿using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public class CollectionVM
    {
        public Static Static { get; set; }
        public List<Product> Products { get; set; }
        public List<Collection> Collections { get; set; }
        public List<Vendor> Vendors { get; set; }
    }
}
