namespace DcoumentAPI.Domain.Dtos
{
    public class DocumentUploadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UploadedBy { get; set; }
        public bool IsApproved { get; set; }
        public int CategoryId { get; set; }

    }
}
