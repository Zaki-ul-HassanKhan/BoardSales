
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class BoardSalesDbContext : DbContext
    {
        public BoardSalesDbContext(DbContextOptions<BoardSalesDbContext> options):base(options)
        {
           
        }
        public DbSet<User> User { get; set; }
        public DbSet<Identity> Identity { get; set; }
        public DbSet<Product> Product { get; set; }

    }
}
