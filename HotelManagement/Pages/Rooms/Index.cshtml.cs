using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Rooms;

public class IndexModel : DI_BasePageModel {
    public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}

    public IList<RoomModel> RoomModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? hotelid) {

        if (hotelid == null) return BadRequest();

        var isAuthorized = User.IsInRole(HotelConstants.HotelManagerRole) || 
                           User.IsInRole(HotelConstants.HotelAdminRole);
        
        RoomModel = await Context.RoomModel
            .Where(r => r.HotelId == hotelid)
            .ToListAsync();
        return Page();
    }
}