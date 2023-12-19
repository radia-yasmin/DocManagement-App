namespace DcoumentAPI.Domain.EntityModels
{
    public class FileUploadModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string UploadedBy { get; set; }
        public bool IsApproved { get; set; }
    }
}
