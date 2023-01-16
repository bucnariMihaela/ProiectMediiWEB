using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectMedii.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "Nume")]
        [RegularExpression(@"^[A-Z][a-z+\s]{3,40}", ErrorMessage = "Numele categoriei de cazare")]
        [Required(ErrorMessage = "Acest câmp este obligatoriu de completat")]
        public string Name { get; set; }
    }
}
