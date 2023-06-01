using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Hotels; 

public class IndexModel : DI_BasePageModel {
    public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}

    public IList<HotelModel> HotelModel { get; set; } = default!;

    public async Task OnGetAsync() {
        var isAuthorized = User.IsInRole(HotelConstants.HotelManagerRole) ||
                           User.IsInRole(HotelConstants.HotelAdminRole);

        if (!isAuthorized) Forbid();

        HotelModel = await Context.HotelModel.ToListAsync();
    }
}