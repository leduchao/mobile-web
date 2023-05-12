using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Models;
//using MobileWeb.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MobileWeb.Areas.Identity.Data;

namespace MobileWeb.Data
{
  public class MobileWebContext : DbContext
  {
    public MobileWebContext(DbContextOptions<MobileWebContext> options)
        : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder builder)
    //{
    //  base.OnConfiguring(builder);
    //}

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //  base.OnModelCreating(modelBuilder);
    //}

    public DbSet<MobileWeb.Models.Category> Category { get; set; } = default!;

    public DbSet<MobileWeb.Models.Product>? Product { get; set; }

    public DbSet<MobileWeb.Models.User>? User { get; set; }

    public DbSet<MobileWeb.Models.Cart>? Cart { get; set; }
    //public DbSet<MobileWeb.Areas.Admin.Models.Cart>? Cart_1 { get; set; }
    //public DbSet<MobileWeb.Areas.Admin.Models.Category>? Category_1 { get; set; }
    //public DbSet<MobileWeb.Areas.Admin.Models.Product>? Product_1 { get; set; }
    //public DbSet<MobileWeb.Areas.Admin.Models.User>? User_1 { get; set; }
  }
}
