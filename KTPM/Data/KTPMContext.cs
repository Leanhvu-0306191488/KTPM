using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace KTPM.Data
{
    public class KTPMContext : DbContext
    {
        public KTPMContext (DbContextOptions<KTPMContext> options)
            : base(options)
        {
        }

        public DbSet<MVC.Models.Invoice> Invoices { get; set; }

        public DbSet<MVC.Models.Account> Accounts { get; set; }

        public DbSet<MVC.Models.InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<MVC.Models.Cart> Carts { get; set; }

        public DbSet<MVC.Models.Product> Products { get; set; }

        public DbSet<MVC.Models.ProductType> ProductTypes { get; set; }
    }
}
