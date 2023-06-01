using HotelManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelManager.Pages.Hotels;

public class DI_BasePageModel : PageModel {
    public DI_BasePageModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager) {
        Context = context;
        AuthorizationService = authorizationService;
        UserManager = userManager;
    }

    protected ApplicationDbContext Context { get; }
    protected IAuthorizationService AuthorizationService { get; }
    protected UserManager<IdentityUser> UserManager { get; }
}