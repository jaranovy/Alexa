using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alexa.Data.Model
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public Group()
        {
        }

        public Group(int id, string name)
        {
            ID = id;
            Name = name;
            Suppliers = new List<Supplier>();
        }
    }
}