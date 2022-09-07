using API_020922.Caching;
using API_020922.Models;
using API_020922.Models.DTOs;
using API_020922.Repositories;
using AutoMapper;

namespace API_020922.Services
{
    public interface IExpenseService
    {        
        public Task<ExpenseCategoryDTO> AddExpenseCategory(ExpenseCategoryDTO expenseCategory);
        public Task<IEnumerable<ExpenseCategoryDTO>> GetAllExpenseCategories();
        public Task<ExpenseCategoryDTO> UpdateExpenseCategory(ExpenseCategoryDTO expenseCategory);
    }

    public class ExpenseService : IExpenseService
    {
        IGenericRepo<ExpenseCategory> repo;
        IMapper mapper;
        ICacheManager cache;
        string key;
        ILogger<ExpenseService> logger;

        public ExpenseService(IGenericRepo<ExpenseCategory> repo,IMapper mapper, ICacheManager cache, ILogger<ExpenseService> logger)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cache = cache;
            key = "";
            this.logger = logger;
        }

        public async Task<ExpenseCategoryDTO> AddExpenseCategory(ExpenseCategoryDTO category)
        {
            var res = await repo.AddAsync(mapper.Map<ExpenseCategory>(category));
            await repo.SaveChangesAsync();
            logger.LogInformation("New expense category added..!!");
            return mapper.Map<ExpenseCategoryDTO>(res);
        }

        public async Task<IEnumerable<ExpenseCategoryDTO>> GetAllExpenseCategories() {
            key = "GetAllExpenseCategories";
            var res = await cache.TryGetAsync<IEnumerable<ExpenseCategoryDTO>>(key);
            if (res == null) 
            {
                res = mapper.Map<IEnumerable<ExpenseCategoryDTO>>(await repo.GetAllAsync());
                await cache.TrySetAsync(key, res);
            }
            return res;
        }

        public async Task<ExpenseCategoryDTO> UpdateExpenseCategory(ExpenseCategoryDTO expenseCategory)
        {
            var category = repo.Update(mapper.Map<ExpenseCategory>(expenseCategory));
            await repo.SaveChangesAsync();
            logger.LogInformation("Expense category updated..!!");
            return mapper.Map<ExpenseCategoryDTO>(category);
        }

    }
}
