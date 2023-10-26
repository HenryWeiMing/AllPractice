namespace AllPractice.Models;

public partial class Work
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public DateTime? UpdateTime { get; set; }
}
