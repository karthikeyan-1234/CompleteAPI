using API_020922.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API_020922.Contexts
{
    public interface IAppDBContext
    {
        DbSet<ExpenseCategory>? ExpenseCategories { get; set; }
        DbSet<Expense>? Expenses { get; set; }

        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry Entry(object obj);
    }
}