using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Rooms;

public class EditModel : DI_BasePageModel {
    public EditModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}

    [BindProperty] public RoomModel RoomModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null || Context.RoomModel == null) return NotFound();

        var roommodel = await Context.RoomModel.FirstOrDefaultAsync(m => m.RoomId == id);
        if (roommodel == null) return NotFound();
        RoomModel = roommodel;

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, RoomModel, HotelConstants.UpdateOperation);

        if (!isAuthorized.Succeeded) return Forbid();
        
        ViewData["HotelId"] = new SelectList(Context.HotelModel, "HotelId", "HotelId");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync() {
        
        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, RoomModel, HotelConstants.UpdateOperation);

        if (!isAuthorized.Succeeded) return Forbid();

        Context.Attach(RoomModel).State = EntityState.Modified;

        try {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!RoomModelExists(RoomModel.RoomId))
                return NotFound();
            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool RoomModelExists(int id) {
        return (Context.RoomModel?.Any(e => e.RoomId == id)).GetValueOrDefault();
    }
}