using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.Data.Models;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatPhongController : ControllerBase
    {
        private readonly WebKaraokeDbContext _context;

        public DatPhongController(WebKaraokeDbContext context)
        {
            _context = context;
        }

        // GET: api/datphong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatPhong>>> GetDatPhongs()
        {
            return await _context.DatPhong
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                .ToListAsync();
        }

        // GET: api/datphong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DatPhong>> GetDatPhong(int id)
        {
            var datPhong = await _context.DatPhong
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                .FirstOrDefaultAsync(dp => dp.DatPhongID == id);

            if (datPhong == null) return NotFound();
            return datPhong;
        }

        // GET: api/datphong/khachhang/5
        [HttpGet("khachhang/{khachHangId}")]
        public async Task<ActionResult<IEnumerable<DatPhong>>> GetDatPhongByKhachHang(int khachHangId)
        {
            return await _context.DatPhong
                .Include(dp => dp.Phong)
                .Where(dp => dp.KhachHangID == khachHangId)
                .ToListAsync();
        }

        // POST: api/datphong
        [HttpPost]
        public async Task<ActionResult<DatPhong>> PostDatPhong(DatPhong datPhong)
        {
            // Validate business rules
            var phong = await _context.Phong.FindAsync(datPhong.PhongID);
            if (phong == null) return BadRequest("Phòng không tồn tại");
            if (phong.TrangThai != "Trong") return BadRequest("Phòng không khả dụng");

            _context.DatPhong.Add(datPhong);
            
            // Update room status
            phong.TrangThai = "DaDat";
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDatPhong), new { id = datPhong.DatPhongID }, datPhong);
        }

        // PUT: api/datphong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatPhong(int id, DatPhong datPhong)
        {
            if (id != datPhong.DatPhongID) return BadRequest();
            _context.Entry(datPhong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/datphong/5/trangthai
        [HttpPatch("{id}/trangthai")]
        public async Task<IActionResult> UpdateTrangThai(int id, [FromBody] string trangThai)
        {
            var datPhong = await _context.DatPhong.FindAsync(id);
            if (datPhong == null) return NotFound();
            
            datPhong.TrangThai = trangThai;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/datphong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatPhong(int id)
        {
            var datPhong = await _context.DatPhong.FindAsync(id);
            if (datPhong == null) return NotFound();
            
            _context.DatPhong.Remove(datPhong);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}