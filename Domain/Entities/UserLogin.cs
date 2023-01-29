using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserLogin
    {
        public int Id { get; set; }
        public int UsertId { get; set; }
        public User User { get; set; }
    
        public DateTime LoginDate { get; set; } = DateTime.Now;
        public DateTime LogoutDate { get; set; } = DateTime.Now;
    }
}
