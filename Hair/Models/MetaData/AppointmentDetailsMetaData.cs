using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hair.Models
{
    public partial class AppointmentDetail : IValidatableObject
    {
        HairContext _context = HairContext.Context;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (_context.Appointment.Where(x=> x.AppointmentId==this.AppointmentId).Any(x => x.Completed == true))
            {
                yield return new ValidationResult("The Appointment is closed for edits"); //, new[] { "Comments" }


            }

            yield return ValidationResult.Success;
        }
    }

    [ModelMetadataTypeAttribute(typeof (AppointmentDetail))]
    public class AppointmentDetailsMetaData
    {
       
        public int AppointmentDetailId { get; set; }

        [Required(ErrorMessage ="Get you forget this!")]
        public int AppointmentId { get; set; }
        public int ProductId { get; set; }

       
        public int ProcedureMinutes { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public double RetailPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public double Total { get; set; }
        public string Comments { get; set; }


        //[Remote("IsClosed","AppointmentDetails",ErrorMessage ="This Appoinment is closed for edits")]
        public Appointment Appointment { get; set; }
        public Product Product { get; set; }

    }


  
}
