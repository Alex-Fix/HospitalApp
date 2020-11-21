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
        private ICollection<Role_User_Mapping> _role_user_mapings;
        public virtual ICollection<Role_User_Mapping> Role_User_Mappings
        {
            get { return _role_user_mapings ?? (_role_user_mapings = new List<Role_User_Mapping>()); }
            set { _role_user_mapings = value; }
        }
        
    }
}
