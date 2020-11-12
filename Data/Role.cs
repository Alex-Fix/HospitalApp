using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<Role_User_Mapping> Role_User_Mappings { get; set; }

        public Role()
        {
            Role_User_Mappings = new List<Role_User_Mapping>();
        }
    }
}
