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
        public string title { get; set; }

        [Required]
        public string Description { get; set; }


        public string Instructions { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        [Required]
        public string start { get; set; }
        [Required]
        public string end { get; set; }

        public string UserId { get; set; }
        public string backgroundColor { get; set; }
    }
}