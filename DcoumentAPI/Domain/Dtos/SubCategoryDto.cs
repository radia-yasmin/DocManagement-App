using System.ComponentModel.DataAnnotations;

namespace DcoumentAPI.Domain.Dtos
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
