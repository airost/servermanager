using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Reservations; 

public class EditModel : DI_BasePageModel {
    public EditModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {
    }

    [BindProperty] public ReservationModel ReservationModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null) return NotFound();

        var reservationModel = await Context.ReservationModel.FirstOrDefaultAsync(m => m.ReservationId == id);
        if (reservationModel == null) return NotFound();
        ReservationModel = reservationModel;

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, ReservationOperations.Update
        );

        if (!isAuthorized.Succeeded) return Forbid();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int id) {
        var reservation = await Context.ReservationModel
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.ReservationId == id);

        if (reservation == null) return NotFound();

        ReservationModel.UserID = reservation.UserID;

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, ReservationOperations.Update
        );

        if (!isAuthorized.Succeeded) return Forbid();

        Context.Attach(ReservationModel).State = EntityState.Modified;

        try {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!ReservationModelExists(ReservationModel.ReservationId)) return NotFound();
            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool ReservationModelExists(int id) {
        return (Context.ReservationModel?.Any(e => e.ReservationId == id)).GetValueOrDefault();
    }
}