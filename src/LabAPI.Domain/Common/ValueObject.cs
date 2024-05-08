namespace LabAPI.Domain.Common;

public abstract class ValueObject
{
    public string Id { get; set; } = new Guid().ToString();
}