using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectMedii.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        [Display(Name = "De la")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public DateTime From { get; set; }
        [Display(Name = "Până la")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public DateTime To { get; set; }
    }
}
