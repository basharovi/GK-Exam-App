using System;

namespace GKExamApp.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public decimal Mark { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
