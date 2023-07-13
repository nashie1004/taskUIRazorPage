using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace taskui.Pages
{
    public class ReleasePageModel : PageModel
    {
        private readonly ILogger<ReleasePageModel> _logger;

        public ReleasePageModel(ILogger<ReleasePageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}