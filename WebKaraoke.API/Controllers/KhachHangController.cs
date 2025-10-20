using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.Data.Models;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhachHangController : ControllerBase
    {
        private readonly WebKaraokeDbContext _context;

        public KhachHangController(WebKaraokeDbContext context)
        {
            _context = context;
        }

        // GET: api/khachhang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
            return await _context.KhachHang.ToListAsync();
        }

        // GET: api/khachhang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null) return NotFound();
            return khachHang;
        }

        // GET: api/khachhang/search?keyword=Nguyen
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<KhachHang>>> SearchKhachHang([FromQuery] string keyword)
        {
            return await _context.KhachHang
                .Where(k => k.HoTen.Contains(keyword) || k.SoDienThoai.Contains(keyword))
                .ToListAsync();
        }

        // POST: api/khachhang
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            _context.KhachHang.Add(khachHang);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKhachHang), new { id = khachHang.KhachHangID }, khachHang);
        }

        // PUT: api/khachhang/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.KhachHangID) return BadRequest();
            _context.Entry(khachHang).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/khachhang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null) return NotFound();
            
            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}