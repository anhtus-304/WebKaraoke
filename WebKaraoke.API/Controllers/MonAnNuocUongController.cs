using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.Data.Models;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonAnNuocUongController : ControllerBase
    {
        private readonly WebKaraokeDbContext _context;

        public MonAnNuocUongController(WebKaraokeDbContext context)
        {
            _context = context;
        }

        // GET: api/monannuocuong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonAnNuocUong>>> GetMonAnNuocUongs()
        {
            return await _context.MonAnNuocUong.ToListAsync();
        }

        // GET: api/monannuocuong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonAnNuocUong>> GetMonAnNuocUong(int id)
        {
            var mon = await _context.MonAnNuocUong.FindAsync(id);
            if (mon == null) return NotFound();
            return mon;
        }

        // GET: api/monannuocuong/danhmuc/DoAn
        [HttpGet("danhmuc/{danhMuc}")]
        public async Task<ActionResult<IEnumerable<MonAnNuocUong>>> GetMonTheoDanhMuc(string danhMuc)
        {
            return await _context.MonAnNuocUong
                .Where(m => m.DanhMuc == danhMuc)
                .ToListAsync();
        }

        // POST: api/monannuocuong
        [HttpPost]
        public async Task<ActionResult<MonAnNuocUong>> PostMonAnNuocUong(MonAnNuocUong mon)
        {
            _context.MonAnNuocUong.Add(mon);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMonAnNuocUong), new { id = mon.MonID }, mon);
        }

        // PUT: api/monannuocuong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonAnNuocUong(int id, MonAnNuocUong mon)
        {
            if (id != mon.MonID) return BadRequest();
            _context.Entry(mon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/monannuocuong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonAnNuocUong(int id)
        {
            var mon = await _context.MonAnNuocUong.FindAsync(id);
            if (mon == null) return NotFound();
            
            _context.MonAnNuocUong.Remove(mon);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}