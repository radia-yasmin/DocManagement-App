using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using DcoumentAPI.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentDbContext _context;
        public DocumentController(DocumentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            return Ok(await _context.Documents.Include(x=>x.Category).Include(x=>x.Users).Where(x => x.IsApproved).ToListAsync());
        }

        [HttpGet("GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments()
        {
            return Ok(await _context.Documents.Include(x => x.Category).Include(x => x.Users).Where(x => !x.IsApproved).ToListAsync());
        }

        [HttpGet("GetUserDocument")]
        public async Task<IActionResult> GetUserDocument(string UserId)
        {
            return Ok(await _context.Documents.Include(x => x.Category).Include(x => x.Users).Where(x => x.UploadedBy == UserId).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }
            var obj = new {Id = document.Id, Title=document.Title, Description = document.Description, FileName = document.FileName, FilePath = document.FilePath, UploadedBy = document.UploadedBy,
                CategoryId = document.CategoryId
            };
            return Ok(obj);
        }


        [HttpGet("ApproveDocument/{id}")]

        public async Task<IActionResult> ApproveDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }
            document.IsApproved = true;
            await _context.SaveChangesAsync();
            return Ok("Document Approved Successfully!");
        }

        [HttpGet("ApplyAgain/{id}")]

        public async Task<IActionResult> ApplyAgain(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }
            document.IsApproved = false;
            document.RejectionReason = null;
            await _context.SaveChangesAsync();
            return Ok("Document Apply Again for approvel Successfully!");
        }

        [HttpPost]
        public async Task<IActionResult> PostDocument(DocumentUploadDto document)
        {
            Document model = new Document();
            model.Title = document.Title;
            model.Description = document.Description;
            model.FileName = document.FileName;
            model.FilePath = document.FilePath;
            model.UploadedBy = document.UploadedBy;
            model.CategoryId = document.CategoryId;
            _context.Documents.Add(model);
            await _context.SaveChangesAsync();

            return Ok("Document Update Successfully");
        }


        [HttpPost("RejectDocument")]
        public async Task<IActionResult> RejectDocument(DocumentRejectDto document)
        {
            var data = await _context.Documents.FindAsync(document.Id);
            data.RejectionReason = document.RejectionReason;
            await _context.SaveChangesAsync();

            return Ok("Document Rejection Reason Update Successfully");
        }


        [HttpPut]
        public async Task<IActionResult> PutDocument(DocumentUploadDto document)
        {
            try
            {
                if (document.Id < 0)
                {
                    return BadRequest();
                }
                var model = await _context.Documents.FindAsync(document.Id);
                model.Title = document.Title;
                model.Description = document.Description;
                model.FileName = document.FileName;
                model.FilePath = document.FilePath;
                model.UploadedBy = document.UploadedBy;
                model.CategoryId = document.CategoryId;

                await _context.SaveChangesAsync();
                return Ok("Document Update Successfully");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Document\\files", document.FilePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if the file does not exist
            }
            else
            {
                System.IO.File.Delete(filePath);
            }
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return Ok("Document Remove Successfully");
        }

        [HttpGet("RemoveDocumentFile/{id}")]
        public async Task<IActionResult> RemoveDocumentFile(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Document\\files", document.FilePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if the file does not exist
            }
            else
            {
                System.IO.File.Delete(filePath);
                return Ok("Document file Remove Successfully");

            }
        }

        [HttpPost("UploadDocument")]
        public IActionResult UploadDocument(IFormFile file)
        {
            string fileName = "";
            try
            {
                fileName = FileHandler.UploadFile(file, Path.Combine(Directory.GetCurrentDirectory(), "Document\\files"), "files");
                return Ok(fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound(); // Return 404 if the document with the specified id is not found
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Document\\files", document.FilePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if the file does not exist
            }

            var memory = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            // Determine the content type based on the file extension
            var contentType = "application/octet-stream";

            // Return the file as a FileStreamResult
            return File(memory, contentType, document.FileName);
        }

    }
}
