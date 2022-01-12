using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Self_Improve_eCommerce.Models.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Improve_eCommerce.Data
{
    public class SelfImproveDbContext : IdentityDbContext<User>
    {
        public SelfImproveDbContext(DbContextOptions<SelfImproveDbContext> options)
            : base(options)
        {
            
        }

        
        public DbSet <Basket> Baskets { get; set; }
        public DbSet <BasketItem> BasketItems { get; set; }
        public DbSet <DeliveryAddres> DeliveryAddres { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            
            base.OnModelCreating(builder);
        }

    }
}
