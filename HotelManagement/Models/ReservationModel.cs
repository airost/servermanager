using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HotelManager.Models;

public class ReservationModel {
    [Key] public int ReservationId { get; set; }

    public DateOnly ReservationFrom { get; set; }
    public DateOnly ReservationTo { get; set; }

    [ForeignKey("HotelModel")] public int HotelID { get; set; }

    public HotelModel Hotel { get; set; }

    [ForeignKey("RoomModel")] public int RoomID { get; set; }

    public RoomModel Room { get; set; }

    [ForeignKey("IdentityUser")] public string UserID { get; set; }

    public IdentityUser User { get; set; }
    public ReservationStatus Status { get; set; }
}

public enum ReservationStatus {
    Submitted,
    Approved,
    Rejected
}