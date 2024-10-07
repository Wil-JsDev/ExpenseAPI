using ExpenseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Persistence.Context
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options) 
        {
            
        }

        #region Models
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region PrimaryKey
            modelBuilder.Entity<Expense>()
                        .HasKey(e => e.ExpenseId);

            modelBuilder.Entity<Category>()
                .HasKey(e => e.CategoryId);
            #endregion

            modelBuilder.Entity<Expense>()
                  .Property(e => e.ExpenseId)
                  .ValueGeneratedOnAdd();

            #region Tables
            modelBuilder.Entity<Expense>().ToTable("Expenses");
            modelBuilder.Entity<Category>().ToTable("Categories");
            #endregion

            #region Relations
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Category)
                .WithMany(r => r.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Fk_Expense");

            #endregion
        }
    }
}
