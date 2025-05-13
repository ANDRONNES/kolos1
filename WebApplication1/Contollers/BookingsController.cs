using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Contollers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingsService _bookingsService;
    
    public BookingsController(IBookingsService bookingsService)
    {
        _bookingsService = bookingsService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingsAsync(int id)
    {
        var bookings = await _bookingsService.GetBookingsAsync(id);
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> PostBooking([FromBody] CreateBookingDTO dto)
    {
        await _bookingsService.PostBooking(dto);
        return Created();
    }
}