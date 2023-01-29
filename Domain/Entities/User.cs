

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; } 
        [Required] 
        public string Name { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string MobileNumber { get; set; }
        public string Password { get; set; } 

        public List<UserRole> UserRoles { get; set; }  
            
    }
}
