using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManager.Models;

public class HotelModel {
    [Key] 
    public int HotelId { get; set; }

    public string HotelName { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public float HotelRating { get; set; }

    [ForeignKey("HotelId")] 
    public ICollection<RoomModel>? HotelRooms { get; set; }
}