using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly DocumentDbContext _context;

        public SubCategoryController(DocumentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetSubCategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            var category = await _context.SubCategories.Include(x=>x.Category).ToListAsync();
            return Ok(category);
        }

        [HttpGet("GetSubCategory/{id}")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            var category = await _context.SubCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostSubCategory(SubCategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            SubCategories model = new SubCategories();
            model.Name = category.Name;
            model.CategoryId = category.CategoryId;
            _context.SubCategories.Add(model);
            await _context.SaveChangesAsync();

            return Ok("Subcategory Create Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> PutSubCategory(SubCategoryDto category)
        {
            if (category.Id < 0)
            {
                return BadRequest();
            }
            SubCategories model = new SubCategories();
            model.Name = category.Name;
            model.Id = category.Id;
            model.CategoryId = category.CategoryId;
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok("Subcategory Update Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var category = await _context.SubCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok("Subcategory Remove Successfully");
        }
    }
}
