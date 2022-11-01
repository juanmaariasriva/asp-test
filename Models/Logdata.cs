using System.ComponentModel.DataAnnotations;

namespace senddatatest.Models
{
    public class Logdata
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Register { get; set; }
        [Required]
        public int Click { get; set; }
        [Required]
        public String Browser { get; set; }
        [Required]
        public String Platform { get; set; }
        public DateTime DateTime { get; set; }
    }
}
