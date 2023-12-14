using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DocumentDbContext _context;

        public CategoryController(DocumentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var category = await _context.Categories.Include(x=>x.SubCategories).ToListAsync();
            return Ok(category);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Category model = new Category();
            model.Name = category.Name;
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();

            return Ok("Category Create Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoryDto category)
        {
            if (category.Id < 0)
            {
                return BadRequest();
            }
            Category model = new Category();
            model.Name = category.Name;
            model.Id = category.Id;
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok("Category Update Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok("Category Remove Successfully");
        }
    }
}
