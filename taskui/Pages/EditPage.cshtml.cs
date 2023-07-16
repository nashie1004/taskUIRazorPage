using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using taskui.Models;
using taskui.Services;

namespace taskui.Pages
{
    public class EditPageModel : PageModel
    {
        private readonly IDataAccess dataAccess_;
        public EditPageModel(IDataAccess dataAccess)
        {
            dataAccess_ = dataAccess;
        }
        // routing
        [BindProperty(SupportsGet = true)]
        public int pageId { get; set; }
        public Header DisplayHeader { get; set; }
        public void OnGet()
        {
            /*
             
            var result = dataAccess_.GetOneReleasePage(pageId);
            if (result == null)
            {
                //
            }
            else
            {
                DisplayHeader = result;
            }
             */
        }
    }
}
