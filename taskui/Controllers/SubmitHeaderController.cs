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
        public async Task<SubmitHeader> SubmitNewHeader([FromBody] SubmitHeader submit)
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
                //var allHeaders = await dataAccess_.GetHeaderList();
                
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

        [HttpPost]
        [Route("base64")]
        public IActionResult UploadImage()
        {
            try
            {
                var files = Request.Form.Files;
                if (files != null && files.Count > 0)
                {
                    var imageUrls = new List<string>();

                    foreach (var imageFile in files)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            imageFile.CopyTo(ms);
                            byte[] imageBytes = ms.ToArray();
                            string base64String = Convert.ToBase64String(imageBytes);

                            // Save the base64String to a database or a file as per your requirements.// Return the image URL to the editor. The URL should be an endpoint that can serve the image// when the editor needs to display it.string imageUrl = "path_to_serve_image_from_base64"; // Replace with the appropriate URL.
                            //imageUrls.Add(imageUrl);
                        }
                    }

                    return Json(new { urls = imageUrls });
                }
            }
            catch (Exception ex)
            {
                // Handle exception as per your application requirements.
            }

            return BadRequest();
        }
    }
}
