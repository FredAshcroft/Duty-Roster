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
    }
}