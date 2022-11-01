using System.ComponentModel.DataAnnotations;

namespace senddatatest.Models
{
    public class Register
    {
        [Key]
        public string id { get; set; }
        public string url { get; set; }
        public int count { get; set; }
    }
}
