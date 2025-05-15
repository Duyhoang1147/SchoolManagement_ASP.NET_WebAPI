namespace SchoolManagerment_WebAPI.Model.Dto{
    public class TeacherGetDto
    {
        public Guid Id { get; set; }
        public string? Fullname { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Subject { get; set; }
    }
}