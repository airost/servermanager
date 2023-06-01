using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace HotelManager.Authorization;

public class
    ReservationCreatorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ReservationModel> {
    private readonly UserManager<IdentityUser> _userManager;

    public ReservationCreatorAuthorizationHandler(UserManager<IdentityUser> userManager) {
        _userManager = userManager;
    }


    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        ReservationModel reservation
    ) {
        if (context.User == null || reservation == null) return Task.CompletedTask;

        if (requirement.Name != ReservationConstants.CreateOperation &&
            requirement.Name != ReservationConstants.ReadOperation &&
            requirement.Name != ReservationConstants.UpdateOperation &&
            requirement.Name != ReservationConstants.DeleteOperation)
            return Task.CompletedTask;

        if (reservation.UserID == _userManager.GetUserId(context.User)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}