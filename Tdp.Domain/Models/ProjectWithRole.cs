namespace Tdp.Domain.Models;

public class ProjectWithRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; }
    }