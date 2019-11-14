using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alexa.Service.Dtos
{
    public class SupplierDto
    {
        public int ID { get; set; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [Display(Name = "Address:")]
        [Required(ErrorMessage = "The Address is required")]
        public string Address { get; set; }

        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "The Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Telephone Number:")]
        [Required(ErrorMessage = "The Telephone Number is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number (NNN NNN NNN)")]
        public string Phone { get; set; }

        [Display(Name = "Groups:")]
        public virtual ICollection<GroupDto> Groups { get; set; }

        [Display(Name = "Selected Groups:")]
        public String SelectedItems { get; set; }

        [Display(Name = "All Groups:")]
        public ICollection<GroupDto> AllGroups { get; set; }

        public SupplierDto()
        {
        }

        public SupplierDto(int id, string name, string address, string email, string phone) : this(id, name, address, email, phone, new List<GroupDto>())
        {
        }

        public SupplierDto(int id, string name, string address, string email, string phone, ICollection<GroupDto> groups)
        {
            ID = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
            Groups = groups;
        }
    }
}