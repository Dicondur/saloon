using System;
using System.Collections.Generic;

namespace Hair.Models
{
    public partial class Product
    {
        public Product()
        {
            AppointmentDetail = new HashSet<AppointmentDetail>();
        }

        public int ProductId { get; set; }
        public bool IsProcedure { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double RetailPrice { get; set; }
        public double WholesalePrice { get; set; }
        public int ProcedureMinutes { get; set; }

        public ICollection<AppointmentDetail> AppointmentDetail { get; set; }
    }
}
