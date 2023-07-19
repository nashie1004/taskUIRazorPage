using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
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

        public List<Header> HeaderList { get; set; } = new List<Header>() { };
        public int FirstHeader { get; set; }
        [BindProperty(SupportsGet = true)]
        public int paginationId { get; set; } = 0;

        public async Task OnGet()
        {
            var tempHeader = await dataAccess_.GetHeaderList();
            var tempList = tempHeader.OrderBy(item => item.ReleaseDate).Reverse().ToList();

            paginationId = paginationId * 5;
            if (paginationId == 0)
            {
                //paginationId = 1;
            }

            HeaderList = tempList; 

            // testing pagination
            try
            {
                //var result = tempList.GetRange(5, 5);
                //await Console.Out.WriteLineAsync($"{result.Count()}");
            } catch (Exception ex) { await Console.Out.WriteLineAsync($"error TC {ex}"); }
            //await Console.Out.WriteLineAsync($"headerCount: {HeaderList.Count()}");

            if (HeaderList.Count != 0)
            {
                var firstItem = HeaderList.OrderBy(item => item.ReleaseDate).Reverse().First(); 
                FirstHeader = firstItem.HeaderId;
            }
        }
    }
}