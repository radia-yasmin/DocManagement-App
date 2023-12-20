using System.ComponentModel.DataAnnotations.Schema;

namespace DcoumentAPI.Domain.EntityModels
{
    public class DocumentUploadModel
    {
        [ForeignKey("Category")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string UploadedBy { get; set; }
        public bool IsApproved { get; set; }
    }
}
