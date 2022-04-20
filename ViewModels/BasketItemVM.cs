using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public class BasketItemVM
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
