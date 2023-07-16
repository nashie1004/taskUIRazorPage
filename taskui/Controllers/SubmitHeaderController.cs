using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using taskui.Models;
using taskui.Services;

namespace taskui.Contoller
{
    [Route("/[controller]")]
    [ApiController]
    public class SubmitHeaderController : Controller
    { 
        /*
        private readonly IDataAccess dataAccess_;
        public SubmitHeaderController(IDataAccess dataAccess)
        {
            dataAccess_ = dataAccess;
        }

        [HttpPost]
        public SubmitHeader SubmitNewHeader([FromBody] SubmitHeader submit)
        {
            try
            {
                ICollection<Detail> tempDetails = new Collection<Detail>();

                foreach(var item in submit.Details) 
                { 
                    tempDetails.Add(
                        new Detail() { 
                            Type = item.Type,
                            Name = item.Name,
                            Description = item.Description,
                        }    
                    );
                }
            
                Header newHeader = new Header()
                {
                    ReleaseName = submit.ReleaseName,
                    ReleaseDate = submit.ReleaseDate,
                    ShortDescription = submit.ShortDescription, 
                    LongDescription = submit.LongDescription,
                    Detail = tempDetails
                };

                dataAccess_.SubmitForm(newHeader);
                return submit;
            } 
            catch (Exception ex)
            {
                return submit;
            }
        }
        */

        [HttpGet]
        [Route("deleteAPage")]
        public IActionResult DeleteAPage()
        {
            //TODO

            //dataAccess
            return Ok("hi delete page here");
            return Redirect("/");
        }

        [HttpGet]
        [Route("editAPage")]
        public IActionResult EditAPage()
        {
            //TODO

            //dataAccess
            return Ok("hi edit page here");
            return Redirect("/");
        }
    }
}
