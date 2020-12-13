using System.Collections.Generic;

namespace GKExamApp.Models
{
    public class User
    {
        public User() => Exams = new List<Exam>();
        public int Id { get; set; }
        public string Name { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<Exam> Exams { get; set; }
    }
}
