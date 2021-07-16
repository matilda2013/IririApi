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
        public EventController(IEventService eventService, UserManager<MemberRegistrationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _eventService = eventService;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }



        [HttpPost]
        [Route("AddNewEvent")]
        public HttpResponseMessage AddNewEvent([FromForm] EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                string myFileName = UploadedEventFile(model);

                var result = new EventModel();

                result.EventTitle = model.EventTitle;
                result.Date = model.Date;
                result.Amount = model.Amount;
                result.EventPicture = myFileName;
                result.EventVenue = model.EventVenue;
                result.EventDescription = model.EventDescription;

               return  _eventService.AddNewEventAsync(result);
            }

            else
            {
                throw new ObjectNotFoundException($"Event could not be added");
            }
        

        }

  

        private string UploadedEventFile(EventViewModel model)
        {
            string myFileName = null;


            if (model.EventImage != null && model.EventImage.Count > 0)
            {
                foreach (IFormFile picture in model.EventImage) {
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
        [Route("GetAllApprovedEvents")]
        public List<EventModel> GetAllApprovedEvents()
        {
            return _eventService.ViewAllApprovedEventsAsync();
        }


        [HttpPut]
        [Route("ApproveEvent")]
        
        public HttpResponseMessage ApproveEvent(ApproveEventViewModel model)
        {
            return _eventService.ApproveEventAsync(model.EventId);
         
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
        public HttpResponseMessage UploadImageToGallery([FromForm] GalleryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string myFileName = UploadedFile(model);

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

                string filepath = Path.Combine($"{webHostEnvironment.ContentRootPath}/Asset/images", $"{fileName}");

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



    }
}
