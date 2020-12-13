using System.ComponentModel.DataAnnotations.Schema;

namespace GKExamApp.Models
{
    public class Question
    {
        public int Id { get; set; }

        [NotMapped]
        public int Index { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string RightAnswer { get; set; }

    }
}
