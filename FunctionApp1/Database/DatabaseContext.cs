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

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //    DbSet<FlatDetails> flatDetails { get; set; }
        //DbSet<Address> Addresses { get; set; }  
    }
}
