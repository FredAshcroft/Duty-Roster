using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutyRoster.Data
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public string Street { get; set; }


        public string TownCity { get; set; }


        public string County { get; set; }

        [Required]
        public string Postcode { get; set; }
    }
}