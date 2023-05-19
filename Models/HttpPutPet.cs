using System.ComponentModel.DataAnnotations;

namespace PracticeWebAPI.Models
{
    public class HttpPutPet
    {
        [Required(ErrorMessage = "Dakhal smiya ndin dymak :)")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
