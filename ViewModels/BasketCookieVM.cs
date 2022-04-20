using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public class BasketCookieVM
    {
        public List<BasketItemVM> BasketItems { get; set; }
        public Static Static { get; set; }
        public double TotalPrice { get; set; }
        public int Count { get; set; }
    }
}
