using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hair.Models;
using Microsoft.AspNetCore.Http;

namespace Hair.Controllers
{
    public class AppointmentDetailsController : Controller
    {
        private readonly HairContext _context;

        public AppointmentDetailsController(HairContext context)
        {
            _context = context;
        }

        // GET: AppointmentDetails
        public async Task<IActionResult> Index(int? apptid)
        {
            //if apptid is passed store to session
            if (apptid != null){
                TempData["message"] = "Appointment ID stored to session";
                HttpContext.Session.SetInt32("AppointmentID", Convert.ToInt32(apptid));

            }
            else
            {
                if (HttpContext.Session.GetInt32("AppointmentID") != null)
                //if(HttpContext.Session.TryGetValue("AppointmentID", out byte[] inByte))
                {
                    //TempData["message"] = "Appointment ID restored from session";
                    apptid = HttpContext.Session.GetInt32("AppointmentID");
                }
                else
                {
                    TempData["message"] = "Please select a appointment";
                    return RedirectToAction("Index","Appointments");
                }
                
            }


            var hairContext = _context.AppointmentDetail
                    .Include(a => a.Appointment)
                    .Include(a => a.Product)
                    .Where(x => x.AppointmentId == apptid);

            ViewBag.apptid = apptid;
            
            ViewBag.tMinutes = hairContext.Sum(x => x.ProcedureMinutes);
            ViewBag.tCost = hairContext.Sum(x => x.RetailPrice);

            return View(await hairContext.ToListAsync());
        }

        // GET: AppointmentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetail = await _context.AppointmentDetail
                .Include(a => a.Appointment)
                .Include(a => a.Product)
                .SingleOrDefaultAsync(m => m.AppointmentDetailId == id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }

            return View(appointmentDetail);
        }

        // GET: AppointmentDetails/Create
        public IActionResult Create(int? apptid)
        {
           

            //For Passing to Create Post Method
            TempData["AppointmentId"] = apptid;
            
            //ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId");
            //ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            return View();
        }

        // POST: AppointmentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int apptid, [Bind("AppointmentDetailId,AppointmentId,ProductId,ProcedureMinutes,RetailPrice,Quantity,Discount,Total,Comments")] AppointmentDetail appointmentDetail)
        {
            //Check for null before converting to int
            if (TempData["AppointmentId"] != null)
                {
                    appointmentDetail.AppointmentId = Convert.ToInt32(TempData["AppointmentId"]);
                }

            //appointmentDetail.AppointmentId = apptid; //throws FK error??

            //Set defaults to zero as these are required attributes
            appointmentDetail.RetailPrice = 0;
            appointmentDetail.ProcedureMinutes = 0;
            appointmentDetail.Total = 0;

            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(appointmentDetail);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Saved to DB";
                    return RedirectToAction(nameof(Index));
                }
            }

            catch(Exception ex)
            {
                TempData["message"] = ex.InnerException.Message;
            }

            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId", appointmentDetail.AppointmentId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", appointmentDetail.ProductId);

            TempData["message"] = "Model Validation dint pass";
            return View(appointmentDetail);
        }

        // GET: AppointmentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetail = await _context.AppointmentDetail.SingleOrDefaultAsync(m => m.AppointmentDetailId == id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId", appointmentDetail.AppointmentId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", appointmentDetail.ProductId);
            return View(appointmentDetail);
        }

        // POST: AppointmentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentDetailId,AppointmentId,ProductId,ProcedureMinutes,RetailPrice,Quantity,Discount,Total,Comments")] AppointmentDetail appointmentDetail)
        {
            if (id != appointmentDetail.AppointmentDetailId)
            {
                return NotFound();
            }

            try
            {

                if (ModelState.IsValid)
                {
                _context.Update(appointmentDetail);
                    await _context.SaveChangesAsync();

                    TempData["message"] = "Saved to DB";//Save success
                return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentDetailExists(appointmentDetail.AppointmentDetailId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.InnerException.Message;
            }

            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId", appointmentDetail.AppointmentId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", appointmentDetail.ProductId);


            return View(appointmentDetail);
        }

        // GET: AppointmentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetail = await _context.AppointmentDetail
                .Include(a => a.Appointment)
                .Include(a => a.Product)
                .SingleOrDefaultAsync(m => m.AppointmentDetailId == id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }

            return View(appointmentDetail);
        }

        // POST: AppointmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var appointmentDetail = await _context.AppointmentDetail
                            .SingleOrDefaultAsync(m => m.AppointmentDetailId == id);

                _context.AppointmentDetail.Remove(appointmentDetail);
                await _context.SaveChangesAsync();

                TempData["message"] = "Deleted";//Delete success
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.InnerException.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentDetailExists(int id)
        {
            return _context.AppointmentDetail.Any(e => e.AppointmentDetailId == id);
        }

        //public JsonResult IsClosed(int apptid)
        //{


        //    return Json(_context.AppointmentDetail
        //        .Where(x => x.AppointmentId == apptid)
        //        .Any(x => x.Appointment.Completed));
            
            
           
        //}
    }
}
