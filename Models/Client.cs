using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectMedii.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Display(Name = "Numele clientului")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Lungimea trebuie sa fie cuprinsă între 6 și 50 de caractere.")]
        [RegularExpression(@"^[A-Z][a-z]{2,}\s[A-Z][a-z]{2,}$", ErrorMessage = "Numele trebuie sa fie de forma: Prenume Nume")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        [Display(Name = "Telefon")]
        [StringLength(10, ErrorMessage = "Format: 07xxxxxxxx")]
        [RegularExpression(@"^07\d{8}", ErrorMessage = "Format: 07xxxxxxxx")]
        public string ContactMobile { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Introduceți o adresă de mail validă")]
        public string ContactMail { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
