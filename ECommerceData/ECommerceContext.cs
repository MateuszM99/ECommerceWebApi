﻿using ECommerceData.Migrations;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceData
{
    public class ECommerceContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionGroup> OptionGroups { get; set; }
        public DbSet<ProductOption> ProductOption { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductOption>().HasKey(key => new { key.OptionId, key.ProductId });

            builder.Entity<CartProduct>().HasKey(key => new { key.CartId, key.ProductId });

            builder.Entity<CartProduct>()
                .HasOne<Product>(p => p.Product)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<CartProduct>()
                .HasOne<ShoppingCart>(c => c.Cart)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(c => c.CartId);

            builder.Entity<ProductOption>()
                .HasOne<Product>(p => p.Product)
                .WithMany(po => po.ProductOptions)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<ProductOption>()
               .HasOne<Option>(o => o.Option)
               .WithMany(po => po.ProductOptions)
               .HasForeignKey(o => o.OptionId);

            builder.Entity<Address>()
                .HasMany(u => u.ApplicationUsers)
                .WithOne(a => a.Address);

            builder.Entity<Category>()
               .HasMany(p => p.Products)
               .WithOne(c => c.Category);

            builder.Entity<OptionGroup>()
               .HasMany(o => o.Options)
               .WithOne(og => og.OptionGroup);

            builder.Entity<ShoppingCart>()
                .HasOne(u => u.AppUser)
                .WithOne(c => c.Cart)
                .HasForeignKey<ShoppingCart>(i => i.UserId);
        }
    }
}
