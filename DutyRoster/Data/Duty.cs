using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutyRoster.Data
{
    public class Duty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ClubId { get; set; }
        public Club Club { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Starttime { get; set; }
        public string Endtime { get; set; }
        public string Instructions { get; set; }

        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        

    }
}