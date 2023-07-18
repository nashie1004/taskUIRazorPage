using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using taskui.Models;
using taskui.Services;
using IronPdf;

namespace taskui.Contoller
{
    [Route("/[controller]")]
    [ApiController]
    public class SubmitHeaderController : Controller
    { 
        
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
        

        [HttpGet]
        [Route("deleteAPage/{pageId}")]
        public IActionResult DeleteAPage(int pageId)
        {
            try
            {
                dataAccess_.DeleteAPage(pageId);
            } catch (Exception ex) { }

            return Redirect("/Edit");
        }

        [HttpPost]
        [Route("submitTableView")]
        public IActionResult TableViewSubmit(SubmitBody dataJSON)
        {
            try
            {
                dataAccess_.SubmitTableView(dataJSON);
            } catch (Exception ex) { }
            return Ok("table view ok");
        }

        [HttpGet]
        [Route("DeleteOneDetail/{detailID}")]
        public IActionResult DeleteOneDetail(int detailID)
        {
            dataAccess_.DeleteDetail(detailID);
            return Redirect("/Edit");
        }

        [HttpGet]
        [Route("AddOneDetail/{headerId}")]
        public IActionResult AddOneDetail(int headerId)
        {
            dataAccess_.AddDetail(headerId);
            return Redirect("/Edit");
        }

        [HttpGet]
        [Route("Print")]
        public IActionResult Print(int headerId = 10)
        {
            var render = new ChromePdfRenderer();
            var pdf = render.RenderHtmlAsPdf("<h1>Hello World</h1> first pdf");
            pdf.SaveAs("htmlPDF.pdf");

            return Redirect("/");
        }
    }
}
