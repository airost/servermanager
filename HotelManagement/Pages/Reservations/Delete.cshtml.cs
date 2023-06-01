using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Reservations; 

public class DeleteModel : DI_BasePageModel {
    public DeleteModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {
    }

    [BindProperty] public ReservationModel ReservationModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null) return NotFound();
        ReservationModel = await Context.ReservationModel.FirstOrDefaultAsync(m => m.ReservationId == id);
        if (ReservationModel == null) return NotFound();

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, ReservationOperations.Delete
        );

        if (!isAuthorized.Succeeded) return Forbid();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id) {
        if (id == null) return NotFound();
        ReservationModel = await Context.ReservationModel.FindAsync(id);

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, ReservationOperations.Delete
        );

        if (!isAuthorized.Succeeded) return Forbid();


        if (ReservationModel != null) {
            Context.ReservationModel.Remove(ReservationModel);
            await Context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}