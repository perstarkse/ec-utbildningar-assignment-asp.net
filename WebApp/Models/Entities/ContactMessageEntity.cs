namespace WebApp.Models.Entities
{
    public class ContactMessageEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
