using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221_PE_1407.Models;

    public class BookDbContext : DbContext
    {
        public BookDbContext (DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public DbSet<PRN221_PE_1407.Models.Book> Book { get; set; }

        public DbSet<PRN221_PE_1407.Models.AccountUser> AccountUser { get; set; }
        public DbSet<PRN221_PE_1407.Models.Publisher> Publisher { get; set; }
    }
