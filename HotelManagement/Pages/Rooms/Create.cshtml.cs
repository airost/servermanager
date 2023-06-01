using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManager.Pages.Rooms;

public class CreateModel : PageModel {
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context) {
        _context = context;
    }

    [BindProperty] public RoomModel RoomModel { get; set; } = default!;

    public IActionResult OnGet() {
        ViewData["HotelId"] = new SelectList(_context.HotelModel, "HotelId", "HotelId");
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync() {
        _context.RoomModel.Add(RoomModel);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}