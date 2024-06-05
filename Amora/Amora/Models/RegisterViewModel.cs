
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Password must be 6+ symbols, 1 uppercase, 1 number.")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
        public string Photo { get; set; }


        public bool IsAnyFieldNotEmpty()
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(this);
                    if (property.Name != "Photo" && !string.IsNullOrWhiteSpace(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
