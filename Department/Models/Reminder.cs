using System.ComponentModel.DataAnnotations;

namespace Department.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public bool IsSent { get; set; } = false; // To track if the email has been sent
    }
}
