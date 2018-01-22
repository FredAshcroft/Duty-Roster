using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyRoster.Models
{
    public class DutyModel
    {
        public int Id { get; set; }
        public int ClubId { get; set; }


        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        public string Instructions { get; set; }

        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }

        public string UserId { get; set; }
    }
}