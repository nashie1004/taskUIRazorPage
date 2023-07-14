using Microsoft.AspNetCore.Mvc.RazorPages;
using taskui.Models;
using taskui.Services;

namespace taskui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataAccess dataAccess_;

        public IndexModel(IDataAccess dataAccess)
        {
            dataAccess_ = dataAccess;
        }

        public List<Header> HeaderList { get; set; }

        public async Task OnGet()
        {
            HeaderList = await dataAccess_.GetHeaderList();
        }
    }
}