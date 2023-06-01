using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace HotelManager.Authorization;

public class ReservationManagerAuthorizationManager :
    AuthorizationHandler<OperationAuthorizationRequirement, ReservationModel> {
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        ReservationModel reservation
    ) {
        if (context.User == null || reservation == null) return Task.CompletedTask;

        if (requirement.Name != ReservationConstants.ApproveOperation &&
            requirement.Name != ReservationConstants.RejectOperation &&
            requirement.Name != ReservationConstants.ReadOperation)
            return Task.CompletedTask;

        if (context.User.IsInRole(ReservationConstants.ReservationManagerRole)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}