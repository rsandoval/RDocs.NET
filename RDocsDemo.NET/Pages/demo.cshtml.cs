using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

using DocumentFormat.OpenXml.Packaging;


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
            _resultMessage =
                "En esta demostraci�n Ud. puede subir un documento en formato docx, txt, o pdf y dejar que R:Docs lo analice.\n\r"
                + "Algunas restricciones de estas capacidades.\n\r"
                + "- Por ahora, s�lo documentos de texto legible - no im�genes\n\r"
                + "- S�lo puede intentar con 10 documentos por d�a.";

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
        public string ResultMessage { get { return _resultMessage; } set { _resultMessage = value; } }
        public string Filename { get; set; }
        public string TypeDescription { get; set; }
        public string DateDescription { get; set; }
        public string NamesDescription { get; set; }
        public string NotaryDescription { get; set; }

        //public async Task OnPostAsync()
        public async Task OnPostAsync()
        {
            Document document = new Document(FileForUpload, DateTime.Today);

            ResultMessage = "Documento con las siguientes caracter�sticas encontradas";
            Filename = FileForUpload.FileName;
            TypeDescription = document.TypeDescription;
            DateDescription = document.IssueDate.ToString("dd MMM yyyy");
            NamesDescription = document.NamedParts;
            NotaryDescription = document.NotaryName;
        }
    }
}