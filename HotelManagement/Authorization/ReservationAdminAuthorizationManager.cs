using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace HotelManager.Authorization; 

public class ReservationAdminAuthorizationManager :
    AuthorizationHandler<OperationAuthorizationRequirement, ReservationModel> {

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        ReservationModel reservation
    ) {
        if (context.User == null || reservation == null) {
            return Task.CompletedTask;
        }

        if (requirement.Name != ReservationConstants.CreateOperation &&
            requirement.Name != ReservationConstants.ReadOperation &&
            requirement.Name != ReservationConstants.UpdateOperation &&
            requirement.Name != ReservationConstants.DeleteOperation &&
            requirement.Name != ReservationConstants.ApproveOperation &&
            requirement.Name != ReservationConstants.RejectOperation) {
            return Task.CompletedTask;
        }

        if (context.User.IsInRole(ReservationConstants.ReservationAdminRole)) {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;

    }
}