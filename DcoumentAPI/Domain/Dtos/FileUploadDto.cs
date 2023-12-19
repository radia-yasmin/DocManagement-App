namespace DcoumentAPI.Domain.Dtos
{
    public class FileUploadDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string UploadedBy { get; set; }
        public bool IsApproved { get; set; }
        public IFormFile File { get; set; }
    }
}
