using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutyRoster.Data
{
    public class Club
    {
        public Club()
        {
            Address = new HashSet<Address>();
            Duties = new HashSet<Duty>();
            AvailableDuties = new HashSet<DutyType>();
        }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual IEnumerable<Address> Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ContactEmail { get; set; }
        public virtual IEnumerable<ApplicationUser> Members { get; set; }
        public virtual IEnumerable<Duty> Duties { get; set; }
        public virtual IEnumerable<DutyType> AvailableDuties { get; set; }
    }
}