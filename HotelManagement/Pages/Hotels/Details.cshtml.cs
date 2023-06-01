using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Pages.Hotels; 

public class DetailsModel : DI_BasePageModel {
    public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}


    public HotelModel HotelModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id) {
        if (id == null) return NotFound();

        var hotelmodel = await Context.HotelModel.FirstOrDefaultAsync(m => m.HotelId == id);
        if (hotelmodel == null)
            return NotFound();
        HotelModel = hotelmodel;
        
        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, HotelModel, HotelOperations.ReadHotel);

        if (!isAuthorized.Succeeded) return Forbid();
        
        return Page();
    }
}