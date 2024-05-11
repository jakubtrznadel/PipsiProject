namespace Amora.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Hobby { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
