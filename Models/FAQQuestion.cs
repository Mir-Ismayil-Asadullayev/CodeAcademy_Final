using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class FAQQuestion
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Question { get; set; }        
        public List<QuestionAnswer> QuestionAnswers { get; set; }

    }
}
