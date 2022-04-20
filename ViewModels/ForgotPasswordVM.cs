using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public class ForgotPasswordVM
    {
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
    }
}
