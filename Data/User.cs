using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Data
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        private ICollection<Role_User_Mapping> _role_user_mapings;

        public virtual ICollection<Role_User_Mapping> Role_User_Mappings
        {
            get { return _role_user_mapings ?? (_role_user_mapings = new List<Role_User_Mapping>()); }
            set { _role_user_mapings = value; }
        }

        [NotMapped]
        public string ShortInfo
        {
            get { return $"Логін: {Login}\tПароль: {Password}"; }
        }

    }
}
