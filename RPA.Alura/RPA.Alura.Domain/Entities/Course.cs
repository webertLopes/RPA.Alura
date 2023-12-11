namespace RPA.Alura.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }        
        public string? Instructor { get; set; }
        public string? Workload { get; set; } 
        public string? Description { get; set; }
        public string? Url { get; set; }
    }
}
