using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alexa.Data.Model
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public Supplier()
        {
        }

        public Supplier(int iD, string name, string address, string email, string phone)
        {
            ID = iD;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
            Groups = new List<Group>();
        }
    }
}