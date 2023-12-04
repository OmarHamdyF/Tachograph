namespace Core.Domain.Entities;

public class TachographData
{
    public TachographData()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public DateTime Date { get; set; }
    public string? Activity { get; set; }
    public string DriverId { get; set; }
    public Driver Driver { get; set; }

}
