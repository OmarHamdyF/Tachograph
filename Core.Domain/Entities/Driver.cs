namespace Core.Domain.Entities;

public class Driver
{
    public string Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<TachographData> TachographDataList { get; set; }

}

