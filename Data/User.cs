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

        [JsonIgnore]
        public virtual ICollection<Role_User_Mapping> Role_User_Mappings { get; set; }

        [NotMapped]
        public List<Role_User_Mapping> Role_User_MappingsList
        {
            get
            {
                return Role_User_Mappings.ToList();
            }
            set
            {
                Role_User_Mappings = value;
            }
        }

        public User()
        {
            Role_User_Mappings = new List<Role_User_Mapping>();
        }
    }
}
