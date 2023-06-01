using HotelManager.Authorization;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Pages.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManager.Pages.Reservations; 

public class CreateModel : DI_BasePageModel {
    public CreateModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
    ) : base(context, authorizationService, userManager) {
        Hotels = new List<HotelModel>(Context.HotelModel.ToList());
    }

    public List<HotelModel> Hotels { get; set; }

    [BindProperty] public int HotelId { get; set; }

    [BindProperty] public List<RoomModel> HotelRooms { get; set; }

    [BindProperty] public int RoomId { get; set; }


    [BindProperty] public ReservationModel ReservationModel { get; set; } = default!;

    public IActionResult OnGet(int hotelid, int? roomid) {
        if (roomid != null) RoomId = roomid.Value;
        HotelRooms = new List<RoomModel>(Context.RoomModel.ToList().FindAll(r => r.HotelId == HotelId));

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync() {
        ReservationModel.HotelID = HotelId;
        ReservationModel.UserID = UserManager.GetUserId(User);

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, ReservationModel, ReservationOperations.Create
        );

        if (!isAuthorized.Succeeded) return Forbid();

        Context.ReservationModel.Add(ReservationModel);
        await Context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}