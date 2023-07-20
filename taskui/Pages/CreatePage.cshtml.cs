using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            Console.WriteLine(IncrementCount);
        }


        /* redo this *
        public IActionResult OnPost(Header data) 
        {
            try
            {
                dataAccess_.SubmitForm(data);
            } 
            catch (Exception ex)
            {
                
            }
            return Redirect("/");
        }*/
    }
}
