using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gp2_project.Models;

namespace gp2_project.Data
{
    public class gp2_projectContext : DbContext
    {
        public gp2_projectContext (DbContextOptions<gp2_projectContext> options)
            : base(options)
        {
        }

        public DbSet<gp2_project.Models.consumer> consumer { get; set; } = default!;

        public DbSet<gp2_project.Models.manager>? manager { get; set; }

        public DbSet<gp2_project.Models.orders>? orders { get; set; }

        public DbSet<gp2_project.Models.payment>? payment { get; set; }

        public DbSet<gp2_project.Models.recycleCompanies>? recycleCompanies { get; set; }

        public DbSet<gp2_project.Models.user>? user { get; set; }
    }
}
