using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_Avancerad.NET.Helper;
using ProjektModels;

namespace Projekt_Avancerad.NET.Data
{
    public class ProjektDbContext : IdentityDbContext<IdentityUser>
    {

        public ProjektDbContext(DbContextOptions<ProjektDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyID = 1,
                CompanyName = "Solviken"
            });

           
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 1,
                FirstName = "Jonas",
                LastName = "Svensson",
                Email = "jonas@gmail.se",
                Phone = "0712345432"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 2,
                FirstName = "Lovisa",
                LastName = "Johansson",
                Email = "lovisa@gmail.se",
                Phone = "0712345444"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 3,
                FirstName = "Göran",
                LastName = "Karlsson",
                Email = "Göran@gmail.se",
                Phone = "0712345666"
            });



            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 1,
                Title = "Meeting",
                Description = "Conference booked for 10+ people",
                StartDate = new DateTime(2024, 5, 20, 13,0,0),
                EndDate = new DateTime(2024, 5, 20, 15,0,0),
                CustomerID = 1
                
            });

            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 2,
                Title = "Meeting",
                Description = "Conference booked for 10+ people",
                StartDate = new DateTime(2024, 5, 24, 13, 0, 0),
                EndDate = new DateTime(2024, 5, 24, 15, 0, 0),
                CustomerID = 2
               
            });

            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 3,
                Title = "Meeting",
                Description = "Conference booked for 10+ people",
                StartDate = new DateTime(2024, 5, 25, 13, 0, 0),
                EndDate = new DateTime(2024, 5, 25, 15, 0, 0),
                CustomerID = 3
               
            });

           
        }
    }
}
