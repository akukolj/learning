﻿using System.Data.Entity;
using Maintenance.Domain;

namespace Maintenance.Data
{
  public class MaintenanceContext : DbContext
  {
    public MaintenanceContext() : base("name=GeekStuffSales") {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
   
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("Maintenance");
      base.OnModelCreating(modelBuilder);
    }
  }

  public class OrderSystemContextConfig : DbConfiguration
  {
    public OrderSystemContextConfig()
    {
       this.SetDatabaseInitializer(new NullDatabaseInitializer<MaintenanceContext>());
    }
    
  }
}