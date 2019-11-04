using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDocsDemo.NET.Data;
using RDocsDemo.NET.Models;

namespace RDocsDemo.NET.Pages.Documents
{
    public class CreateModel : PageModel
    {
        private readonly RDocsDemo.NET.Data.RDocsDemoNETContext _context;

        public CreateModel(RDocsDemo.NET.Data.RDocsDemoNETContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Document Document { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Document.Add(Document);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}