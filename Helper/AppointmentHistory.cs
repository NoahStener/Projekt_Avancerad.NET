using System.ComponentModel.DataAnnotations;

namespace Projekt_Avancerad.NET.Helper
{
    public class AppointmentHistory
    {
        [Key]
        public int Id { get; set; }
        public int AppointmentId {  get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; } 
        public string ChangeType { get; set; }  
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
