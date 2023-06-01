using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelManager.Pages.Hotels; 

public class CreateModel : DI_BasePageModel {
    public CreateModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {}


    [BindProperty] public HotelModel HotelModel { get; set; } = default!;

    public IActionResult OnGet() {
        
        var isAuthorized = User.IsInRole(HotelConstants.HotelManagerRole) ||
                           User.IsInRole(HotelConstants.HotelAdminRole);

        if (!isAuthorized) Forbid();
        
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync() {

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, HotelModel, HotelOperations.CreateHotel
        );

        if (!isAuthorized.Succeeded) 
            return Forbid();
        
        Context.HotelModel.Add(HotelModel);
        await Context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}