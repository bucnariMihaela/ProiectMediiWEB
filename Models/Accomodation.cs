using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectMedii.Models
{
    public class Accomodation
    {
        public int ID { get; set; }
        [Display(Name = "Nume")]
        [RegularExpression(@"^[A-Z][a-z+\s]{3,40}", ErrorMessage = "Numele cazarii trebuie să înceapă cu literă mare, să conțină doar litere sau caracterul + și să fie cuprins între 4 și 40 de caractere")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public string Name { get; set; }
        [Display(Name = "Rating-uri")]
        [RegularExpression(@"\d{1,2}", ErrorMessage = "Nr. de rating-uri trebuie să aibă 1 sau 2 cifre")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public int RatersCount { get; set; }
        [Display(Name = "Rating")]
        [RegularExpression(@"\d{1,2}", ErrorMessage = "Rating-ul mediu trebuie să aibă 1 sau 2 cifre")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public int Rating { get; set; }
        [Display(Name = "Adresa")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public string Address { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        public int? CategoryID { get; set; }

        [Display(Name = "Categoria Cazarii")]
        public Category Category { get; set; }
    }
}
