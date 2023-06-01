using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Reservations; 

public class DetailsModel : DI_BasePageModel {
    public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {
    }

    public ReservationModel ReservationModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null) return NotFound();

        ReservationModel = await Context.ReservationModel.FirstOrDefaultAsync(m => m.ReservationId == id);
        if (ReservationModel == null) return NotFound();

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, ReservationOperations.Read
        );

        if (!isAuthorized.Succeeded) return Forbid();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, ReservationStatus status) {
        ReservationModel = await Context.ReservationModel.FindAsync(id);

        if (ReservationModel == null) return NotFound();

        var reservationOperation = status == ReservationStatus.Approved
            ? ReservationOperations.Approve
            : ReservationOperations.Reject;

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, reservationOperation
        );

        if (isAuthorized.Succeeded == false) return Forbid();

        await Context.SaveChangesAsync();
        return Page();
    }
}