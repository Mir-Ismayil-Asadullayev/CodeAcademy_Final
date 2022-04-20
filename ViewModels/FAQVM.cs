using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public class FAQVM
    {
        public List<Product> Products { get; set; }
        public Static Static { get; set; }
        public List<Collection> Collections { get; set; }
        public List<FAQAnswer> FAQAnswers { get; set; }
        public List<FAQQuestion> FAQQuestions { get; set; }
        public List<Vendor> Vendors { get; set; }
    }
}
