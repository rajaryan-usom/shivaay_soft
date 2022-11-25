using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShivaaySoft.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaaySoft.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryEntity> categories { get; set; }
        public DbSet<ProductEntity> product { get; set; }
    }
}
