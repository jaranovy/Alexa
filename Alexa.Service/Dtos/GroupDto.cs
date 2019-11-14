using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alexa.Service.Dtos
{
    public class GroupDto
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [Display(Name = "Suppliers:")]
        public virtual ICollection<SupplierDto> Suppliers { get; set; }

        [Display(Name = "Selected Suppliers:")]
        public String SelectedItems { get; set; }

        [Display(Name = "All Suppliers:")]
        public ICollection<SupplierDto> AllSuppliers { get; set; }

        public GroupDto()
        {
        }

        public GroupDto(int id, string name) : this(id, name, new List<SupplierDto>())
        {
        }

        public GroupDto(int id, string name, ICollection<SupplierDto> suppliers)
        {
            ID = id;
            Name = name;
            Suppliers = suppliers;
        }
    }
}