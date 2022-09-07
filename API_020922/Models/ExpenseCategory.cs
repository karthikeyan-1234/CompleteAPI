namespace API_020922.Models
{
    public class ExpenseCategory
    {
        public int id { get; set; }
        public string? name { get; set; }

        public ICollection<Expense>? Expense_objs { get; set; }
    }
}
