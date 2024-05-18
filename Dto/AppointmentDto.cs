namespace Projekt_Avancerad.NET.Dto
{
    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
