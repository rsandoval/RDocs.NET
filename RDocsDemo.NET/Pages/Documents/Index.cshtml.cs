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
    public class IndexModel : PageModel
    {
        private readonly RDocsDemo.NET.Data.RDocsDemoNETContext _context;

        public IndexModel(RDocsDemo.NET.Data.RDocsDemoNETContext context)
        {
            _context = context;
        }

        public IList<Document> Document { get;set; }

        public async Task OnGetAsync()
        {
            Document = await _context.Document.ToListAsync();
        }
    }
}
