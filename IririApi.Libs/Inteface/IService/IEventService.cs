using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model.IService
{
    public interface IEventService
    {

   
        EventViewModel ViewEventsByIdAsync(Guid id);
  

        List<EventModel> ViewPastEventsAsync();
       
        List<EventModel> ViewUpcomingEventsAsync();


        List<EventModel> ViewAllEventsAsync();

        HttpResponseMessage UpdateEventAsync(Guid id, UpdateViewModel model);

        HttpResponseMessage DeleteEventAsync(Guid id);
        HttpResponseMessage DeleteEventDuesAsync(Guid id);

        HttpResponseMessage UpdateEventDuesAsync(Guid id, DueViewModel eventModel);

        HttpResponseMessage AddNewEventAsync(EventModel model);

        HttpResponseMessage AddEventDuesAsync(Due model, string MemberId);

        List<Announcement> ViewAllAnnoucementsAsync();

        HttpResponseMessage AddAnnoucementAsync(Announcement model);

        HttpResponseMessage DeleteAnnoucement(Guid id);

        HttpResponseMessage UploadImageToGalleryAsync(Gallery model);

        List<Gallery> ViewGalleryAsync();

        HttpResponseMessage ApproveEventAsync(Guid EventId);

        List<EventModel> ViewAllPendingEventsAsync();

        List<EventModel> ViewAllApprovedEventsAsync();
    }
}
