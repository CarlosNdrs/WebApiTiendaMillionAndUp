using Microsoft.EntityFrameworkCore;
using WebApiTienda.Models;

namespace WebApiTienda.Context
{
    public class PurchaseContext : DbContext
    {
        public PurchaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    }
}
