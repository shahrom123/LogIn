using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        
        public List<UserRole> UserRoles { get; set; }  

        public List<RolePermission> RolePermissions { get; set; }
        

    }
}
