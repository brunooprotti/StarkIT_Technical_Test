namespace StarkIT.Domain.Models
{
    public class Names
    {
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public required string Name { get; set; }

    }

    public class ResponseDb
    {
        public ICollection<Names>? Response { get; set; }
    }
    public enum Gender { F, M };
}
