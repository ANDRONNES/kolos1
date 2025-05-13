namespace WebApplication1.Models.DTOs;

public class CreateBookingDTO
{
    int BookingId { get; set; }
    int GuestId { get; set; }
    string EmpoloyeeNumber { get; set; }
    List<AttractionsDTO> attractions { get; set; }
    
}