using System.ComponentModel.DataAnnotations;

namespace DcoumentAPI.Domain.EntityModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public virtual ICollection<SubCategories> SubCategories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
