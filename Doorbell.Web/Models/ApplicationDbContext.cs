using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Doorbell.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            builder.Entity<Rings>(entity =>
            {
                entity.HasKey(d => d.timestamp);
            });

            base.OnModelCreating(builder);

        }

        public virtual DbSet<Rings> RingList { get; set; }
    }
}
