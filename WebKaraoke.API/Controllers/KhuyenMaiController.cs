using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.Data.Models;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly WebKaraokeDbContext _context;

        public KhuyenMaiController(WebKaraokeDbContext context)
        {
            _context = context;
        }

        // GET: api/khuyenmai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhuyenMai>>> GetKhuyenMais()
        {
            return await _context.KhuyenMai.ToListAsync();
        }

        // GET: api/khuyenmai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhuyenMai>> GetKhuyenMai(int id)
        {
            var khuyenMai = await _context.KhuyenMai.FindAsync(id);
            if (khuyenMai == null) return NotFound();
            return khuyenMai;
        }

        // GET: api/khuyenmai/dangapdung
        [HttpGet("dangapdung")]
        public async Task<ActionResult<IEnumerable<KhuyenMai>>> GetKhuyenMaiDangApDung()
        {
            var now = DateTime.Now;
            return await _context.KhuyenMai
                .Where(km => km.NgayBatDau <= now && km.NgayKetThuc >= now)
                .ToListAsync();
        }

        // POST: api/khuyenmai
        [HttpPost]
        public async Task<ActionResult<KhuyenMai>> PostKhuyenMai(KhuyenMai khuyenMai)
        {
            _context.KhuyenMai.Add(khuyenMai);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKhuyenMai), new { id = khuyenMai.KM_ID }, khuyenMai);
        }

        // PUT: api/khuyenmai/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhuyenMai(int id, KhuyenMai khuyenMai)
        {
            if (id != khuyenMai.KM_ID) return BadRequest();
            _context.Entry(khuyenMai).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/khuyenmai/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhuyenMai(int id)
        {
            var khuyenMai = await _context.KhuyenMai.FindAsync(id);
            if (khuyenMai == null) return NotFound();
            
            _context.KhuyenMai.Remove(khuyenMai);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}