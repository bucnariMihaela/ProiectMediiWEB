using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Data;
using ProiectMedii.Models;

namespace ProiectMedii.Pages.Accomodations
{
    public class EditModel : PageModel
    {
        private readonly ProiectMedii.Data.ProiectMediiContext _context;

        public EditModel(ProiectMedii.Data.ProiectMediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Accomodation Accomodation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Accomodation = await _context.Accomodation.
                Include(a => a.Category).FirstOrDefaultAsync(m => m.ID == id);

            if (Accomodation == null)
            {
                return NotFound();
            }

            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Accomodation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccomodationExists(Accomodation.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AccomodationExists(int id)
        {
            return _context.Accomodation.Any(e => e.ID == id);
        }
    }
}
