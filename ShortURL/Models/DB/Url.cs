using System.ComponentModel.DataAnnotations;

namespace ShortURL.Models.DB
{
    public class Url
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указан Url")]
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }

        public string GuidString { get; set; }
        public DateTime CreationDate { get; set; }
        public int TransitionСount { get; set; }
    }
}
