using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;
using WebKaraoke.DTO;

namespace WebKaraoke.Business.Services // This must match the folder structure
{
    public class PhongService
    {
        private readonly WebKaraokeDbContext _context;

        public PhongService(WebKaraokeDbContext context)
        {
            _context = context;
        }

        public async Task<List<PhongDto>> GetAvailableRoomsAsync()
        {
            var rooms = await _context.Phongs
                .Where(p => p.TrangThai == "Trống")
                .Select(p => new PhongDto
                {
                    PhongID = p.PhongID,
                    TenPhong = p.TenPhong,
                    LoaiPhong = p.LoaiPhong,
                    TrangThai = p.TrangThai
                })
                .ToListAsync();

            return rooms;
        }
    }
}