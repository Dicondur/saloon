using System;
using System.Collections.Generic;

namespace Hair.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointment = new HashSet<Appointment>();
            Staff = new HashSet<Staff>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string PreferredStylist { get; set; }
        public string Comments { get; set; }
        public bool CanAdvertiseOnline { get; set; }

        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<Staff> Staff { get; set; }
    }
}
