using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace IririApi.Libs.Service
{
    public class EventService : IEventService
    {
        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<EventModel> _eventrepository;
        private readonly IRepository<Announcement> _annoucerepository;
        private readonly IRepository<Gallery> _galleryrepository;
        public EventService(AuthenticationContext DbContext)
        {

            _DbContext = DbContext;
            _eventrepository = new Repository<EventModel>(DbContext);
            _annoucerepository = new Repository<Announcement>(DbContext);
            _galleryrepository = new Repository<Gallery>(DbContext);
        }



        public HttpResponseMessage AddNewEventAsync(EventModel model)
        {
            _DbContext.EventModels.Add(model);
            _DbContext.SaveChanges();
            var response = new HttpResponseMessage();
            response.Headers.Add("CreateEventMessage", "Successfuly Added!!!");
            return response;
        }


        public HttpResponseMessage UpdateEventAsync(Guid id, UpdateViewModel eventModel)
        {
            try
            {
          

                EventModel myevent = _DbContext.EventModels.FirstOrDefault(e => e.EventId == id);

                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"No Member With id{id} exists");
                }

                else
                { 
                    myevent.EventPicture = eventModel.EventPicture;
                    myevent.EventTitle = eventModel.EventTitle;
                    myevent.EventVenue = eventModel.EventVenue;
                    myevent.EventDescription = eventModel.EventDescription;
                    myevent.Amount = eventModel.Amount;
                    myevent.Date = eventModel.Date;

                   _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("UpdateMessage", "Successfuly Updated!!!");
                    return response;



                }

            }

            catch (Exception ex)
            {
                throw ex;

            }

           
        }


        public HttpResponseMessage DeleteEventAsync(Guid id)
        {
            try
            {


                EventModel myevent = _DbContext.EventModels.FirstOrDefault(e => e.EventId == id);

                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"No Event With id{id} exists");
                }

                else
                {

                    _DbContext.EventModels.Remove(myevent);
                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("DeleteMessage", "Successfuly Deleted!!!");
                    return response;

                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }


        //public Guid ViewAllEventsAsync(Guid id)
        //{
        //  var eventList = _eventrepository.GetById(id);
        //    return eventList.EventId;

        //}


        public List<EventModel> ViewAllEventsAsync()
        {
            var eventList = _eventrepository.GetAll();
            return eventList.ToList();

        }


        public HttpResponseMessage AddEventDuesAsync(Due model, string MemberId)
        {
             try
             {
                 var result = new Due();
                 result.MembershipId = MemberId;
                 result.NumberOfMonths = model.NumberOfMonths;
                 result.DatePaid = model.DatePaid;
                 result.Amount = model.Amount;

                 _DbContext.Dues.Add(result);
                 _DbContext.SaveChanges();
                var response = new HttpResponseMessage();
                response.Headers.Add("CreateEvenDueMessage", "Successfuly Added!!!");
                return response;

            }

            catch (Exception ex)
             {
                throw ex;
             }


        }

        public HttpResponseMessage UpdateEventDuesAsync(Guid id, DueViewModel eventModel)
        {
            try
            {


              Due myevent = _DbContext.Dues.FirstOrDefault(e => e.DueId == id);

                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"No EventDue With id{id} exists");
                }

                else
                {


                    myevent.NumberOfMonths = eventModel.NumberOfMonths;
                    myevent.DatePaid = eventModel.DatePaid;
                    myevent.Amount = eventModel.Amount;
                  
                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("UpdateMessage", "Successfuly Updated!!!");
                    return response;



                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }



        public HttpResponseMessage DeleteEventDuesAsync(Guid id)
        {
            try
            {

                Due myevent = _DbContext.Dues.FirstOrDefault(e => e.DueId == id);

                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"No EventDue With id{id} exists");
                }

                else
                {

                    _DbContext.Dues.Remove(myevent);
                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("DeleteMessage", "EventDues Successfully Deleted!!!");
                    return response;

                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }


        public List<Announcement> ViewAllAnnoucementsAsync()
        {
             var annouceList = _annoucerepository.GetAll();
             return annouceList.ToList();

        }


        public HttpResponseMessage AddAnnoucementAsync(Announcement model)
        {
      
            
            try
            {   
            var result = new Announcement();

            result.AnnouceDate = model.AnnouceDate;
            result.Annoucement = model.Annoucement;
           _DbContext.Annoucements.Add(result);
           _DbContext.SaveChanges();
            var response = new HttpResponseMessage();
             response.Headers.Add("AnnouncementMessage", "Successfuly Added!!!");
             return response;


            }
        catch (Exception ex)
        {
            throw ex;
        }


    }



        public HttpResponseMessage DeleteAnnoucement(Guid id)
        {
            try
            {

                Due myevent = _DbContext.Dues.FirstOrDefault(e => e.DueId == id);

                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"No Annoucement With id{id} exists");
                }

                else
                {

                    _DbContext.Dues.Remove(myevent);
                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("DeleteMessage", "Annoucement Successfully Deleted!!!");
                    return response;

                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }


        public HttpResponseMessage UploadImageToGalleryAsync(Gallery gallery)
        {
        _DbContext.Gallerys.Add(gallery);
        _DbContext.SaveChanges();
         var response = new HttpResponseMessage();
         response.Headers.Add("UploadedMessage", "Successfuly Uploaded!!!");
         return response;
        }

        public List<Gallery> ViewGalleryAsync()
        {
            var galleryList = _galleryrepository.GetAll();
            return galleryList.ToList();

        }


    }
}


    

