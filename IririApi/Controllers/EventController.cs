using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IririApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private UserManager<MemberRegistrationUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
       // private readonly IHostingEnvironment webHostEnvironment;
        public EventController(IEventService eventService, UserManager<MemberRegistrationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _eventService = eventService;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }



        [HttpPost]
        [Route("AddNewEvent")]
        public HttpResponseMessage AddNewEvent( EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                string myFileName = UploadedEventFile(model.EventBase64, model.EventTitle);

                var result = new EventModel();

                result.EventTitle = model.EventTitle;
                result.Date = model.Date;
                result.Amount = model.Amount;
                result.EventPicture = myFileName;
                result.EventVenue = model.EventVenue;
                result.EventDescription = model.EventDescription;
                result.EventBase64 = myFileName;

               return  _eventService.AddNewEventAsync(result);
            }

            else
            {
                throw new ObjectNotFoundException($"Event could not be added");
            }
        

        }

  

        private string UploadedEventFile(string base64, string title)
        {
            string myFileName = title + "_" + Guid.NewGuid().ToString()+".jpg";
            byte[] imageBytes = Convert.FromBase64String(base64);
             string filepath = Path.Combine($"{webHostEnvironment.WebRootPath}/Asset/images", $"{myFileName}");
          
            System.IO.File.WriteAllBytes(filepath, imageBytes);
      

            return myFileName;
        }

        [HttpPut]
        [Route("EditAnEvent")]
        public HttpResponseMessage UpdateEvent(Guid id, UpdateViewModel model)
        {
           return _eventService.UpdateEventAsync(id,model);

        }

        [HttpDelete]
        [Route("DeleteAnEvent")]
        public HttpResponseMessage DeleteEvent(Guid id)
        {
            return _eventService.DeleteEventAsync(id);

        }

        [HttpGet]
        [Route("ViewEvents")]
        public List<EventModel> GetAllEvents()
        {
            return _eventService.ViewAllEventsAsync();
        }

        [HttpGet]
        [Route("GetAllPendingEvents")]
        public List<EventModel> GetAllPendingEvents()
        {
            return _eventService.ViewAllPendingEventsAsync();
        }

        [HttpGet]
        [Route("ApproveEvent")]
        
        public HttpResponseMessage ApproveEvent(Guid  Id)
        {
            return _eventService.ApproveEventAsync(Id, true);
         
        }


        [HttpGet]
        [Route("ViewEventsById")]
        public EventViewModel GetEventsById(Guid id)
        {
            return _eventService.ViewEventsByIdAsync(id);
        }


        [HttpGet]
        [Route("ViewPastEvents")]
        public List<EventModel> GetPastEvents()
        {
            return _eventService.ViewPastEventsAsync();
        }

        [HttpGet]
        [Route("ViewUpcomingEvents")]
        public List<EventModel> GetUpComingEvents()
        {
            return _eventService.ViewUpcomingEventsAsync();
        }

        [HttpPost]
        [Route("SetUpEventDues")]
        public HttpResponseMessage AddEventDues([FromBody] Due model)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
             var Username = _userManager.FindByIdAsync(userId);
            string MemberId = Username.Id.ToString();
         
            return _eventService.AddEventDuesAsync(model, MemberId);
        }

        [HttpPut]
        [Route("EditEventDues")]
        public HttpResponseMessage UpdateEventDue(Guid id, UpdateViewModel model)
        {
            return _eventService.UpdateEventAsync(id, model);

        }


        [HttpDelete]
        [Route("DeleteEventDues")]
        public HttpResponseMessage DeleteEventDues(Guid id)
        {
            return _eventService.DeleteEventDuesAsync(id);

        }

        [HttpPost]
        [Route("CreateAnnoucement")]
        public HttpResponseMessage  CreateAnnoucement([FromBody] Announcement model)
        {

            return _eventService.AddAnnoucementAsync(model);
        }


        [HttpGet]
        [Route("ViewAnnoucement")]
        public List<Announcement> ViewAnnoucement()
        {
            return _eventService.ViewAllAnnoucementsAsync();
        }

        [HttpDelete]
        [Route("DeleteAnnoucement")]
        public HttpResponseMessage DeleteAnnoucement(Guid id)
        {
            return _eventService.DeleteAnnoucement(id);

        }


        [HttpPost]
        [Route("UploadImageToGallery")]
        public HttpResponseMessage UploadImageToGallery(GalleryViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                string myFileName = "";
                foreach (var item in model.base64)
                {
                    myFileName =  UploadedGalleryFile(item, model.Event);
                }
               

                Gallery gallery = new Gallery
                {
                    FileName = model.FileName,
                    FileTitle = model.FileTitle,
                    Description = model.Description,
                    Event = model.Event,
                    FilePicture = myFileName
                };

                return _eventService.UploadImageToGalleryAsync(gallery);
            }
            else
            {

                throw new ObjectNotFoundException($"Image Couldnt be Uploaded");
            }
        }

        private string UploadedFile(GalleryViewModel model)
        {
            string myFileName = null;


            if (model.FileImage != null && model.FileImage.Count > 0 )
            {
                foreach(IFormFile picture in model.FileImage) {
                    string fileImageFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Asset/images");
                    myFileName = Guid.NewGuid().ToString() + "_" + picture.FileName;
                    string myfilePath = Path.Combine(fileImageFolder, myFileName);
                    using (var myfileStream = new FileStream(myfilePath, FileMode.Create))
                    {
                        picture.CopyTo(myfileStream);
                    }
                }
            }
            return myFileName;
        }

        [HttpGet("GetEventImageByName")]
        public IActionResult GetEventImage(string fileName)
        {
            try
            {
              //  string filepath = Path.Combine($"{webHostEnvironment.WebRootPath}/Asset/images", $"{myFileName}");//Path.Combine(_hostingEnvironment.WebRootPath, "/uploads/images\\" + id + ".jpg");
                string filepath = Path.Combine($"{webHostEnvironment.WebRootPath}/Asset/images", $"{fileName}");

                var fileBytes = System.IO.File.ReadAllBytes(filepath);
                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", fileName);
            }
            catch (Exception e)
            {
                string filename = "NoImage.png";

                var filePath = Path.Combine(webHostEnvironment.ContentRootPath, filename);


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
        }





        [HttpGet]
        [Route("ViewGallery")]
        public List<Gallery> ViewGallery()
        {
            return _eventService.ViewGalleryAsync();
        }

        private string UploadedGalleryFile( string base64, string title)
        {
           // count = 1;
            //count++;
            string myFileName = title + ".jpg";
            byte[] imageBytes = Convert.FromBase64String(base64);
            string filepath = Path.Combine($"{webHostEnvironment.WebRootPath}/Asset/images", $"{myFileName}");

            System.IO.File.WriteAllBytes(filepath, imageBytes);


            return myFileName;
        }

    }
}
