namespace Core.Application.Dtos;

public class TachographDataDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Activity { get; set; }
    public int DriverId { get; set; }
}

