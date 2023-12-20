using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcoumentAPI.Domain.EntityModels
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UploadedBy { get; set; }
        public bool IsApproved { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
