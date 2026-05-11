namespace Booker.Domain.Entity;

public class Book
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public int PublishYear { get; set; }
    public Status Status { get; set; }

    public override string ToString()
    {
        return $"[{Id}] \"{Name}\" by {Author} ({PublishYear}) — {Status}";
    }
}

public enum Status
{
    Available = 0,
    Borrowed = 1
}