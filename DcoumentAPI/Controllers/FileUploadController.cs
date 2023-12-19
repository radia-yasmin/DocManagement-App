using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile([FromForm] FileUploadDto model)
        {
            try
            {
                if (model.File == null || model.File.Length == 0)
                {
                    return BadRequest("File is missing or empty.");
                }

                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.File.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                // Store only the file path in the database
                model.FilePath = Path.Combine("uploads", uniqueFileName);

                // Here you can save the file information (file path) to a database or perform other actions
                // For example, you can store file metadata in a database table

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
