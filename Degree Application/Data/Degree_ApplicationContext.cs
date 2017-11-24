using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Degree_Application.Models
{
    public class Degree_ApplicationContext : DbContext
    {
        public Degree_ApplicationContext (DbContextOptions<Degree_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Degree_Application.Models.AccountModel> AccountModel { get; set; }
    }
}
