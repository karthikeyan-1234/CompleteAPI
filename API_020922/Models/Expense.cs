namespace API_020922.Models
{
    public class Expense
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int category_id { get; set; }

        public ExpenseCategory? ExpenseCategory_obj { get; set; }
    }
}
