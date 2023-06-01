using HotelManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace HotelManager.Authorization;

public class HotelAuthorizationHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, HotelModel>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        HotelModel resource)
    {
        if (context.User == null || resource == null) {
            return Task.CompletedTask;
        }

        if (requirement.Name != HotelConstants.CreateOperation && 
            requirement.Name != HotelConstants.ReadOperation &&
            requirement.Name != HotelConstants.UpdateOperation && 
            requirement.Name != HotelConstants.DeleteOperation) {
            return Task.CompletedTask;
        }

        if (context.User.IsInRole(HotelConstants.HotelManagerRole) 
        ||  context.User.IsInRole(HotelConstants.HotelAdminRole)) {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}