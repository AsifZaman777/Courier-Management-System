using Courier_Management_System.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Courier_Management_System.Data
{
    public class CourierDbContext : DbContext
    {
        public CourierDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Shipment> Shipments { get; set; }
    }
}
