using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class FAQAnswer
    {
        public int Id { get; set; }
        [StringLength(maximumLength:400)]
        [Required]
        public string Answer { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
