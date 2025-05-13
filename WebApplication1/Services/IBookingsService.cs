using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface IBookingsService
{
    public Task<BookingsDTO> GetBookingsAsync(int id);
    public Task PostBooking(CreateBookingDTO dto);
    
}