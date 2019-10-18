using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiClinicGAP.Models;
using WebApiClinicGAP.Models.Custom;

namespace WebApiClinicGAP.Controllers
{
    public class AppointmentsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Appointments
        public IQueryable<Appointments> GetAppointments()
        {
            return db.Appointments;
        }

        // GET: api/Appointments/5
        [ResponseType(typeof(Appointments))]
        public IHttpActionResult GetAppointments(int id)
        {
            Appointments appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return NotFound();
            }

            return Ok(appointments);
        }

        // PUT: api/Appointments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointments(int id, Appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointments.idAppointment)
            {
                return BadRequest();
            }

            db.Entry(appointments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Appointments
        [ResponseType(typeof(Appointments))]
        public IHttpActionResult PostAppointments(Appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointments.idAppointment }, appointments);
        }

        [Route("api/Appointments/CreateAppoint")]
        [HttpPost]
        public IHttpActionResult CreateAppointment(AppointmentCustom appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int typeId = db.AppointmentsTypes.Where(at => at.name.ToLower() == appointment.appointmentType.ToLower()).FirstOrDefault().idAppointmentType;

            Appointments _appointment = new Appointments {
                fk_idPatient = appointment.idPatient,
                fk_idDoctor = 1,
                fk_idAppointmentType = typeId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                isActive = true
            };

            db.Appointments.Add(_appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = _appointment.idAppointment }, _appointment);
        }

        // DELETE: api/Appointments/5
        [ResponseType(typeof(Appointments))]
        public IHttpActionResult DeleteAppointments(int id)
        {
            Appointments appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointments);
            db.SaveChanges();

            return Ok(appointments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentsExists(int id)
        {
            return db.Appointments.Count(e => e.idAppointment == id) > 0;
        }
    }
}