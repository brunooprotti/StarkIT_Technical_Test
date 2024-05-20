namespace StarkIT.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public required string Name { get; set; }

    }

    public class ResponseDb
    {
        public ICollection<User>? Response { get; set; }
    }
    public enum Gender { F, M };
}
