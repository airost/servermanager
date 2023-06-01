using System.ComponentModel.DataAnnotations;

namespace HotelManager.Models;

public class RoomModel {
    [Key] public int RoomId { get; set; }


    public int HotelId { get; set; }
    public HotelModel Hotel { get; set; }

    public int HotelFloor { get; set; }
    public int RoomNumber { get; set; }
    public float RoomPrice { get; set; }
}