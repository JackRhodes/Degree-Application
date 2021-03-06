﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Degree_Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Degree_Application.Data
{
    public class Degree_ApplicationContext : IdentityDbContext<AccountModel>
    {
        
        public Degree_ApplicationContext(DbContextOptions<Degree_ApplicationContext> options)
            : base(options)
        {
        }


        //Create database tables
        public DbSet<ItemModel> Items { get; set; }

        public DbSet<ImageModel> Image { get; set; }

        // public DbSet<Degree_Application.Models.ApplicationUser> AccountModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            //Prevent unecessary fields in the database
            builder.Entity<AccountModel>()
                .Ignore(x => x.AccessFailedCount)
                .Ignore(x => x.ConcurrencyStamp)
                .Ignore(x => x.EmailConfirmed)
                .Ignore(x => x.LockoutEnabled)
                .Ignore(x => x.LockoutEnd)
                .Ignore(x => x.PhoneNumber)
                .Ignore(x => x.PhoneNumberConfirmed)
                .Ignore(x => x.TwoFactorEnabled)
                //Custom table name
                .ToTable("Users");
            

        }
    }

}
