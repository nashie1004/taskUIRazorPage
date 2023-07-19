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
            var tempHeader = await dataAccess_.GetHeaderList();
            HeaderList = tempHeader.OrderBy(item => item.ReleaseDate).Reverse().ToList();

            //await Console.Out.WriteLineAsync($"\n editCount: {HeaderList.Count()}\n");
        }
    }
}
