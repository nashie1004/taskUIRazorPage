using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using taskui.Models;
using taskui.Services;

namespace taskui.Pages
{
    public class TableViewModel : PageModel
    {
        private readonly IDataAccess dataAccess_;

        public TableViewModel(IDataAccess dataAccess)
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
