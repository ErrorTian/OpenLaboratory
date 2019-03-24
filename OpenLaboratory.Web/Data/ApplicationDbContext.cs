using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenLaboratory.Web.Controllers;
using OpenLaboratory.Web.Models;

namespace OpenLaboratory.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
    }
}
