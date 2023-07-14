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

        public void OnGet()
        {
        }

        // redo this
        public async Task<IActionResult> OnPost(Header data) 
        {
            try
            {
                await dataAccess_.SubmitForm(data);
            } 
            catch (Exception ex)
            {
                //
            }
            return Redirect("/");
        }
    }
}
