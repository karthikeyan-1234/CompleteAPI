using API_020922.Hubs;
using API_020922.Misc;
using API_020922.Models;
using API_020922.Models.DTOs;
using API_020922.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;

#pragma warning disable CS8604 // Possible null reference argument.
namespace API_020922.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        IExpenseService expenseService;
        IStringLocalizer<ExpenseCategoryController> localizer;
        IHubContext<InformHub> hub;

        public ExpenseCategoryController(IExpenseService expenseService,IStringLocalizer<ExpenseCategoryController> localizer,IHubContext<InformHub> hub)
        {
            this.expenseService = expenseService;
            this.localizer = localizer;
            this.hub = hub;
        }

        [HttpPost("AddExpenseCategory",Name = "AddExpenseCategory")]
        public async Task<IActionResult> AddExpenseCategory(ExpenseCategoryDTO category)
        {
            var exp = await expenseService.AddExpenseCategory(category);
            return Ok(exp);
        }

        [HttpGet("GetAllExpenseCategories", Name = "GetAllExpenseCategories")]
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            IList<ExpenseCategoryDTO> res = (IList<ExpenseCategoryDTO>) await expenseService.GetAllExpenseCategories();

            //LocalizerHelper<ExpenseCategoryDTO> test = new LocalizerHelper<ExpenseCategoryDTO>();
            //test.Localize(localizer, res);

            for (int i = 0; i < res.Count; i++)
            {
                res[i].name = localizer[res[i].name];
            }

            await hub.Clients.Group("MyGroup").SendAsync("Receive", "Test");

            //await hub.Clients.All.SendAsync("Receive", "Test");

            return Ok(res);
        }

        [HttpPut("UpdateExpenseCategory",Name = "UpdateExpenseCategory")]
        public async Task<IActionResult> UpdateExpenseCategory(ExpenseCategoryDTO expenseCategory)
        {
           var exp_category = await expenseService.UpdateExpenseCategory(expenseCategory);
            return Ok(exp_category);
        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
}
