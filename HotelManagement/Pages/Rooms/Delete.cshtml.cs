using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Rooms;

public class DeleteModel : PageModel {
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context) {
        _context = context;
    }

    [BindProperty] public RoomModel RoomModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null || _context.RoomModel == null) return NotFound();

        var roommodel = await _context.RoomModel.FirstOrDefaultAsync(m => m.RoomId == id);

        if (roommodel == null)
            return NotFound();
        RoomModel = roommodel;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id) {
        if (id == null || _context.RoomModel == null) return NotFound();
        var roommodel = await _context.RoomModel.FindAsync(id);

        if (roommodel != null) {
            RoomModel = roommodel;
            _context.RoomModel.Remove(RoomModel);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}