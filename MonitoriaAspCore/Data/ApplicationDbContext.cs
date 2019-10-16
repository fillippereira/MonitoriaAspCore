using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoriaAspCore.Models;
using MonitoriaAspCore.Areas.Monitoria.Models;

namespace MonitoriaAspCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MonitoriaAspCore.Areas.Monitoria.Models.Bloco> Bloco { get; set; }
    }
}
