using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectMedii.Models
{
    public class Appointment
    {
        public int ID { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Oră start")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "Oră finish")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        public int ClientID { get; set; }

        [Display(Name = "Numele clientului")]
        public Client Client { get; set; }

        public int? AccomodationID { get; set; }

        [Display(Name = "Numele cazarii")]
        public Accomodation Accomodation { get; set; }
    }
}
