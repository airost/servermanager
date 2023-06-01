using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages;

public class SearchModel : PageModel {
    public SearchModel(ApplicationDbContext ctx) {
        Ctx = ctx;
    }

    public ApplicationDbContext Ctx { get; set; }

    [BindProperty] public List<HotelModel> Hotels { get; set; } = default!;

    [BindProperty] public int? HotelId { get; set; }

    public List<RoomModel> Rooms { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? hotelId) {
        if (hotelId != null) {
            HotelId = hotelId;
            Rooms = await Ctx.RoomModel
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            Rooms.RemoveAll(room => Ctx.ReservationModel.ToList().Exists(res => res.RoomID == room.RoomId));
        }

        Hotels = await Ctx.HotelModel.ToListAsync();
        return Page();
    }
}