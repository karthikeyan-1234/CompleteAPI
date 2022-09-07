using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace API_020922.Models.DTOs
{
    public class ExpenseCategoryDTO
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
}
