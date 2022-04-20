namespace MyProject.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int FAQAnswerId { get; set; }
        public int FAQQuestionId { get; set; }
        public FAQAnswer FAQAnswer { get; set; }
        public FAQQuestion FAQQuestion { get; set; }
    }
}
