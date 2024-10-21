﻿using ExpenseAPI.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name:"Users");
            });

            builder.Entity<IdentityRole>(roles =>
            {
                roles.ToTable(name:"Roles");
            });

            builder.Entity<IdentityUserRole<string>>(roles =>
            {
                roles.ToTable(name: "UserRoles");
            });

            builder.Entity<IdentityUserLogin<string>>(roles =>
            {
                roles.ToTable(name: "UserLogins");
            });

        }
    }
}
