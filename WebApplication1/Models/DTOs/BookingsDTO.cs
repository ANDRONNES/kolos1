namespace WebApplication1.Models.DTOs;

public class BookingsDTO
{
    public DateTime Date { get; set; }
    public List<GuestsDTO> Guests { get; set; } 
    public List<EmployeesDTO> Employees{ get; set; }
    public List<AttractionsDTO> Attractions{ get; set; }
}