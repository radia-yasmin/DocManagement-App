using System.ComponentModel.DataAnnotations;

namespace DcoumentAPI.Domain.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
