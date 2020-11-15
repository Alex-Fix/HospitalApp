using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role_User_Mapping> Role_User_Mappings { get; set; }

        public User()
        {
            Role_User_Mappings = new List<Role_User_Mapping>();
        }
    }
}
