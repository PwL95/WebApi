using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    /// <summary>
    /// Dto for Create and update
    /// </summary>
    public class CommandWriteDto
    {
        [Required]
        [MaxLength(50)]
        public string HowTo { get; set; }
        
        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}