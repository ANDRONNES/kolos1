using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class BookingsService : IBookingsService
{
    private readonly string _connectionString;

    public BookingsService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<BookingsDTO> GetBookingsAsync(int id)
    {
        int counter = 0;
        var ids = new List<int>();
        BookingsDTO booking = null;
       var bookings = new List<BookingsDTO>();
       Boolean isSame = false;
        string query =
            @"select b.booking_id, b.date, g.first_name,g.last_name,g.date_of_birth,e.first_name,e.last_name,e.employee_number,
                        a.name,a.price,ba.amount
                        from Booking b join Guest g ON b.guest_id = g.guest_id
                        join Employee e on b.employee_id = e.employee_id
                        join Booking_Attraction ba on ba.booking_id = b.booking_id
                        join Attraction a on ba.attraction_id = a.attraction_id
                        where b.booking_id = @id";
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id",id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (ids.Contains(reader.GetInt32(0)) && counter > 0)
                        {
                            isSame = true;
                        }
                        int bookingId = reader.GetInt32(0);
                        ids.Add(bookingId);
                        
                        // var booking = bookings.FirstOrDefault(b => b.booking_id == bookingId);
                        if(isSame && counter > 0)
                        {
                             booking = new BookingsDTO()
                            {
                                Date = reader.GetDateTime(1),
                                Guests = new List<GuestsDTO>(),
                                Employees = new List<EmployeesDTO>(),
                                Attractions = new List<AttractionsDTO>()
                            };
                            bookings.Add(booking);
                        }

                        if (booking != null)
                        {
                            booking.Guests.Add(new GuestsDTO
                            {
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                                DateOfBirth = reader.GetDateTime(4),
                            });
                            booking.Employees.Add(new EmployeesDTO
                            {
                                FirstName = reader.GetString(5),
                                LastName = reader.GetString(6),
                                EmployeeNumber = reader.GetString(7),
                            });
                            booking.Attractions.Add(new AttractionsDTO
                            {
                                Name = reader.GetString(8),
                                Price = reader.GetDecimal(9),
                                Amount = reader.GetInt32(10)
                            });
                        }
                        
                        counter++;
                    }
                }
            }
        }
        return booking;
    }

    public Task PostBooking(CreateBookingDTO dto)
    {
        string query = @"Insert Into Booking(booking_id,guest_id,employee_number)
                         values(@b_id,@g_id,@e_num)";
        string queryForAttractions = "Insert Into Booking_Attraction(name) values (@name)";
        string queryForBookingAttractions = "Insert Into Booking_Attraction(Amount) values (@amount)";
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(queryForAttractions, conn))
            {
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    
                }
            }
        }
        return null;
    }
}