using API_020922.Models;
using Microsoft.EntityFrameworkCore;

namespace API_020922.Contexts
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public DbSet<Expense>? Expenses { get; set; }
        public DbSet<ExpenseCategory>? ExpenseCategories { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Expenses = modelBuilder.Entity<Expense>();
            var ExpenseCategories = modelBuilder.Entity<ExpenseCategory>();

            Expenses.HasKey(e => e.id).IsClustered();
            Expenses.Property(e => e.id).UseIdentityColumn();

            ExpenseCategories.HasKey(e => e.id).IsClustered();
            ExpenseCategories.Property(e => e.id).UseIdentityColumn();

            Expenses.HasOne(e => e.ExpenseCategory_obj).WithMany(e => e.Expense_objs).HasForeignKey(e => e.category_id);
        }
    }
}
