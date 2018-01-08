using System;
using System.Collections.Generic;

namespace Hair.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int StaffId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? DateHired { get; set; }
        public DateTime? DateTerminated { get; set; }
        public string ReasonForTermination { get; set; }
        public bool IsStylist { get; set; }
        public double HourlyChargeoutRate { get; set; }
        public double HourlySalary { get; set; }
        public string Comments { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
    }
}
