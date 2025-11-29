namespace Api_TaskFlow_DotNet.Models;

public class Project
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreateAt { get; set; }
}
