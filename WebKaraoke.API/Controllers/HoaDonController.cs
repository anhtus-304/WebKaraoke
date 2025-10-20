using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.Data.Models;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoaDonController : ControllerBase
    {
        private readonly WebKaraokeDbContext _context;

        public HoaDonController(WebKaraokeDbContext context)
        {
            _context = context;
        }

        // GET: api/hoadon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDon>>> GetHoaDons()
        {
            return await _context.HoaDon
                .Include(h => h.DatPhong)
                .Include(h => h.NhanVien)
                .Include(h => h.KhuyenMai)
                .ToListAsync();
        }

        // GET: api/hoadon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDon>> GetHoaDon(int id)
        {
            var hoaDon = await _context.HoaDon
                .Include(h => h.DatPhong)
                    .ThenInclude(dp => dp.Phong)
                .Include(h => h.DatPhong)
                    .ThenInclude(dp => dp.KhachHang)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(ct => ct.MonAnNuocUong)
                .FirstOrDefaultAsync(h => h.HoaDonID == id);

            if (hoaDon == null) return NotFound();
            return hoaDon;
        }

        // GET: api/hoadon/5/chitiet
        [HttpGet("{id}/chitiet")]
        public async Task<ActionResult<IEnumerable<ChiTietHoaDon>>> GetChiTietHoaDon(int id)
        {
            var chiTiet = await _context.ChiTietHoaDon
                .Where(ct => ct.HoaDonID == id)
                .Include(ct => ct.MonAnNuocUong)
                .ToListAsync();
            return Ok(chiTiet);
        }

        // POST: api/hoadon
        [HttpPost]
        public async Task<ActionResult<HoaDon>> PostHoaDon(HoaDon hoaDon)
        {
            _context.HoaDon.Add(hoaDon);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHoaDon), new { id = hoaDon.HoaDonID }, hoaDon);
        }

        // POST: api/hoadon/5/chitiet
        [HttpPost("{id}/chitiet")]
        public async Task<ActionResult<ChiTietHoaDon>> AddChiTietHoaDon(int id, ChiTietHoaDon chiTiet)
        {
            chiTiet.HoaDonID = id;
            _context.ChiTietHoaDon.Add(chiTiet);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChiTietHoaDon), new { id = chiTiet.HoaDonID }, chiTiet);
        }

        // PUT: api/hoadon/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoaDon(int id, HoaDon hoaDon)
        {
            if (id != hoaDon.HoaDonID) return BadRequest();
            _context.Entry(hoaDon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/hoadon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon == null) return NotFound();
            
            _context.HoaDon.Remove(hoaDon);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}