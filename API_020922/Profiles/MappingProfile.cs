using API_020922.Models;
using API_020922.Models.DTOs;
using AutoMapper;

namespace API_020922.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<ExpenseDTO, Expense>();
            CreateMap<ExpenseCategory, ExpenseCategoryDTO>();
            CreateMap<ExpenseCategoryDTO, ExpenseCategory>();
        }
    }
}
