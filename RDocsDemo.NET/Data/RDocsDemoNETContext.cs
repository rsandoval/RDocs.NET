using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDocsDemo.NET.Models;

namespace RDocsDemo.NET.Data
{
    public class RDocsDemoNETContext : DbContext
    {
        public RDocsDemoNETContext (DbContextOptions<RDocsDemoNETContext> options)
            : base(options)
        {
        }

        public DbSet<RDocsDemo.NET.Models.Document> Document { get; set; }
    }
}
