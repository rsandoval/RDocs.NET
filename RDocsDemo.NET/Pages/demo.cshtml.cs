using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;

using RDocsDemo.NET.Data;
using RDocsDemo.NET.Models;

namespace RDocsDemo.NET.Pages
{
    public class DemoModel : PageModel
    {
        private string _resultMessage = "Documento a mostrar";
        private readonly RDocsDemo.NET.Data.RDocsDemoNETContext _context;
        private IHostingEnvironment _environment;

        public IActionResult OnGet()
        {
            return Page();
        }

        public DemoModel(RDocsDemo.NET.Data.RDocsDemoNETContext context, IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        /*public IList<Document> Document { get; set; }

        public async Task OnGetAsync()
        {
            Document = await _context.Document.ToListAsync();
        }*/

        [BindProperty]
        public IFormFile FileForUpload { get; set; }
        public string DocumentType { get; set; }
        public string ResultMessage { get { return _resultMessage; } set { _resultMessage = value; } }

        public async Task OnPostAsync()
        {
            Document document = new Document();
            document.Filename = FileForUpload.FileName;
            document.LoadDate = DateTime.Today;

            var contents = new StringBuilder();
            using (var reader = new StreamReader(FileForUpload.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    contents.AppendLine(reader.ReadLine());
            }

            ContentCharacterizer characterizer = new ContentCharacterizer();

            document.Type = characterizer.GetDocumentType(contents.ToString());

            DocumentType = document.Type;

            ResultMessage = "El documento es de tipo " + characterizer.GetTypeDescription(document.Type);

        }


        /*        public async Task<IActionResult> OnPostAsync()
                {
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
                */
    }
}