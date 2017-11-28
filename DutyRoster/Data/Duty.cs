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

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        public string Instructions { get; set; }

        
        public string Dutytype { get; set; }

        [Required]
        public string Date { get; set; }
    }
}