using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutyRoster.Data
{
    public class DutyType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        public string Instructions { get; set; }
    }
}