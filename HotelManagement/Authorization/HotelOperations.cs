using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace HotelManager.Authorization;

public class HotelOperations {
    public static OperationAuthorizationRequirement CreateHotel = new() {
        Name = HotelConstants.CreateOperation
    };

    public static OperationAuthorizationRequirement ReadHotel = new() {
        Name = HotelConstants.ReadOperation
    };

    public static OperationAuthorizationRequirement UpdateHotel = new() {
        Name = HotelConstants.UpdateOperation
    };

    public static OperationAuthorizationRequirement DeleteHotel = new() {
        Name = HotelConstants.DeleteOperation
    };
}

public class HotelConstants {
    public static readonly string CreateOperation = "CreateHotel";
    public static readonly string ReadOperation = "ReadHotel";
    public static readonly string UpdateOperation = "UpdateHotel";
    public static readonly string DeleteOperation = "DeleteHotel";

    public static readonly string HotelManagerRole = "HotelManager";
    public static readonly string HotelAdminRole = "HotelAdmin";
}