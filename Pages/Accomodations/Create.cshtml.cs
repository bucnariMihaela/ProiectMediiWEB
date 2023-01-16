using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectMedii.Data;
using ProiectMedii.Models;

namespace ProiectMedii.Pages.Accomodations
{
    public class CreateModel : PageModel
    {
        private readonly ProiectMedii.Data.ProiectMediiContext _context;

        public CreateModel(ProiectMedii.Data.ProiectMediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Accomodation Accomodation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Accomodation.Add(Accomodation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public IActionResult OnGet()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ClientName");
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name");
            return Page();
        }
    }
}
