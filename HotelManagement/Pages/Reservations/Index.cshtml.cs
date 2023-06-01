using System.Collections.ObjectModel;
using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Reservations;

public class IndexModel : DI_BasePageModel {
    public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}

    public Collection<ReservationModel> ReservationModel { get; set; } = new();

    public async Task OnGetAsync() {
        if (Context.ReservationModel != null) {
            var reservations = await Context.ReservationModel.ToListAsync();
            foreach (var res in reservations) {
                var isAuth = await AuthorizationService.AuthorizeAsync(
                    User, res, ReservationOperations.Read);
                if (isAuth.Succeeded)
                    ReservationModel.Add(res);
            }
        }
    }
}