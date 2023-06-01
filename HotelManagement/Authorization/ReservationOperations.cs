using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace HotelManager.Authorization;

public class ReservationOperations {
    public static OperationAuthorizationRequirement Create = new() {
        Name = ReservationConstants.CreateOperation
    };

    public static OperationAuthorizationRequirement Read = new() {
        Name = ReservationConstants.ReadOperation
    };

    public static OperationAuthorizationRequirement Update = new() {
        Name = ReservationConstants.UpdateOperation
    };

    public static OperationAuthorizationRequirement Delete = new() {
        Name = ReservationConstants.DeleteOperation
    };

    public static OperationAuthorizationRequirement Approve = new() {
        Name = ReservationConstants.ApproveOperation
    };

    public static OperationAuthorizationRequirement Reject = new() {
        Name = ReservationConstants.RejectOperation
    };
}

public class ReservationConstants {
    public static readonly string CreateOperation = "CreateReservation";
    public static readonly string ReadOperation = "ReadReservation";
    public static readonly string UpdateOperation = "UpdateReservation";
    public static readonly string DeleteOperation = "DeleteReservation";

    public static readonly string ApproveOperation = "ApproveReservation";
    public static readonly string RejectOperation = "RejectReservation";

    public static readonly string ReservationManagerRole = "ReservationManager";
    public static readonly string ReservationAdminRole = "ReservationAdmin";
}