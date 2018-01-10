using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutyRoster.Data
{
    public class DutyGroup
    {
        public DutyGroup()
        {
            Duties = new HashSet<Duty>();
        }
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<Duty> Duties { get; set; }

    }
}
 