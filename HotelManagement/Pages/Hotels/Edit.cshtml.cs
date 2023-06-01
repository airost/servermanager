using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Hotels; 

public class EditModel : DI_BasePageModel {
    public EditModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}

    [BindProperty] public HotelModel HotelModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null) return NotFound();

        var hotelmodel = await Context.HotelModel.FirstOrDefaultAsync(m => m.HotelId == id);
        if (hotelmodel == null) return NotFound();

        HotelModel = hotelmodel;

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, HotelModel, HotelOperations.UpdateHotel);

        if (!isAuthorized.Succeeded) return Forbid();
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync() {
        
        
        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, HotelModel, HotelOperations.UpdateHotel);

        if (!isAuthorized.Succeeded) return Forbid();
        
        Context.Attach(HotelModel).State = EntityState.Modified;

        try {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!HotelModelExists(HotelModel.HotelId)) return NotFound();
            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool HotelModelExists(int id) {
        return (Context.HotelModel?.Any(e => e.HotelId == id)).GetValueOrDefault();
    }
}