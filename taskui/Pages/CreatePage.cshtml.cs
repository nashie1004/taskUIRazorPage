using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Tracing;
using taskui.Models;
using taskui.Services;

namespace taskui.Pages
{
    public class CreatePageModel : PageModel
    {
        private readonly IDataAccess dataAccess_;
        public CreatePageModel(IDataAccess dataAccess)
        {
            dataAccess_ = dataAccess;
        }

        [BindProperty(SupportsGet = true)]
        public int IncrementCount { get; set; } = 0;

        public void OnGet()
        {
        }
        public IActionResult OnPostAddButton()
        {
            IncrementCount = IncrementCount + 1;
            //Console.WriteLine("\add detail button\n");

            return Redirect($"/create/{IncrementCount}");
        }
        public IActionResult OnPostRemoveButton() 
        {
            IncrementCount = IncrementCount - 1;
            //Console.WriteLine("\nremove detail button\n");

            return Redirect($"/create/{IncrementCount}");
        }

        public IActionResult Test()
        {
            return Redirect($"/create/{IncrementCount}");
        }
    }
}
