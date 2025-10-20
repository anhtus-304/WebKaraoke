using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.Data.Models;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhongController : ControllerBase
    {
        private readonly WebKaraokeDbContext _context;

        public PhongController(WebKaraokeDbContext context)
        {
            _context = context;
        }

        // GET: api/phong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phong>>> GetPhongs()
        {
            return await _context.Phong.ToListAsync();
        }

        // GET: api/phong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phong>> GetPhong(int id)
        {
            var phong = await _context.Phong.FindAsync(id);
            if (phong == null) return NotFound();
            return phong;
        }

        // GET: api/phong/trong
        [HttpGet("trong")]
        public async Task<ActionResult<IEnumerable<Phong>>> GetPhongTrong()
        {
            return await _context.Phong
                .Where(p => p.TrangThai == "Trong")
                .ToListAsync();
        }

        // GET: api/phong/loai/VIP
        [HttpGet("loai/{loaiPhong}")]
        public async Task<ActionResult<IEnumerable<Phong>>> GetPhongTheoLoai(string loaiPhong)
        {
            return await _context.Phong
                .Where(p => p.LoaiPhong == loaiPhong)
                .ToListAsync();
        }

        // POST: api/phong
        [HttpPost]
        public async Task<ActionResult<Phong>> PostPhong(Phong phong)
        {
            _context.Phong.Add(phong);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPhong), new { id = phong.PhongID }, phong);
        }

        // PUT: api/phong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhong(int id, Phong phong)
        {
            if (id != phong.PhongID) return BadRequest();
            _context.Entry(phong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/phong/5/trangthai
        [HttpPatch("{id}/trangthai")]
        public async Task<IActionResult> UpdateTrangThai(int id, [FromBody] string trangThai)
        {
            var phong = await _context.Phong.FindAsync(id);
            if (phong == null) return NotFound();
            
            phong.TrangThai = trangThai;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/phong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhong(int id)
        {
            var phong = await _context.Phong.FindAsync(id);
            if (phong == null) return NotFound();
            
            _context.Phong.Remove(phong);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}