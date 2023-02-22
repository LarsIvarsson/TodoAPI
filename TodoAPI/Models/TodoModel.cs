namespace TodoAPI.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}


