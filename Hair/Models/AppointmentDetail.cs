using System;
using System.Collections.Generic;

namespace Hair.Models
{
    public partial class AppointmentDetail
    {
        public int AppointmentDetailId { get; set; }
        public int AppointmentId { get; set; }
        public int ProductId { get; set; }
        public int ProcedureMinutes { get; set; }
        public double RetailPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public string Comments { get; set; }

        public Appointment Appointment { get; set; }
        public Product Product { get; set; }
    }
}
