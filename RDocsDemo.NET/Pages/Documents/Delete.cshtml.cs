using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RDocsDemo.NET.Data;
using RDocsDemo.NET.Models;

namespace RDocsDemo.NET.Pages.Documents
{
    public class DeleteModel : PageModel
    {
        private readonly RDocsDemo.NET.Data.RDocsDemoNETContext _context;

        public DeleteModel(RDocsDemo.NET.Data.RDocsDemoNETContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Document Document { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Document = await _context.Document.FirstOrDefaultAsync(m => m.ID == id);

            if (Document == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Document = await _context.Document.FindAsync(id);

            if (Document != null)
            {
                _context.Document.Remove(Document);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
