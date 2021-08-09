//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IririApi.Libs.Model
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {


        }

        public DbSet<MemberRegistrationUser> MemberRegistrationUsers { get; set; }

        public DbSet<Gallery> Gallerys { get; set; }

        public DbSet<EventModel> EventModels { get; set; }

        public DbSet<EmailLog> EmailLogs { get; set; }

        public DbSet<EventPayment> EventPayments { get; set; }

        public DbSet<PaymentGatewayStore> PaymentGatewayStores { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        public DbSet<Announcement> Annoucements { get; set; }

        public DbSet<Due> Dues { get; set; }
        public DbSet<EventGallery> EventGalleries { get; set; }

        public DbSet<EventPaymentPlan> EventPaymentPlans { get; set; }

        public DbSet<MembershipPlanPayment> MembershipPlanPayments { get; set; }

        public DbSet<PaymentPlan> PaymentPlans{ get; set; }
    }

}