using FunctionApp1.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<FlatDetails> Flats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=otodom;Trusted_Connection=True;");
        }
    }
}
