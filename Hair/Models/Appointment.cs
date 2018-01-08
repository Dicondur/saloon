using System;
using System.Collections.Generic;

namespace Hair.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            AppointmentDetail = new HashSet<AppointmentDetail>();
        }

        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan? ScheduledStartTime { get; set; }
        public int ProcedureMinutes { get; set; }
        public TimeSpan? ScheduledEndTime { get; set; }
        public double FinalTotal { get; set; }
        public double TotalBeforeTax { get; set; }
        public double TaxRate { get; set; }
        public bool Completed { get; set; }

        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public ICollection<AppointmentDetail> AppointmentDetail { get; set; }
    }
}
