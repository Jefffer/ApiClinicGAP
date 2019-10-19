using ClinicGAPDataAcces;
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
        private PatientsController patientsController = new PatientsController();

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

        [Route("api/Appointments/GetAppointments")]
        [HttpGet]
        public List<AppointmentCustom> GetCustomAppointments()
        {            
            List<Appointments> appointments = db.Appointments.ToList();
            List<AppointmentCustom> _appointments = new List<AppointmentCustom>();
            
            foreach (Appointments app in appointments)
            {
                AppointmentCustom _app = new AppointmentCustom {
                    idAppointment = app.idAppointment,
                    idPatient = app.fk_idPatient,
                    patientName = patientsController.GetPatientName(app.fk_idPatient),
                    appointmentType = GetAppointmentTypeName(app.fk_idAppointmentType),
                    doctorName = GetDoctorName(app.fk_idDoctor),
                    AppointmentDateTime = app.AppointmentDateTime
                };
                _appointments.Add(_app);
            }

            return _appointments;
        }

        private string GetDoctorName(int id)
        {
            Doctors doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return null;
            }
            return doctor.doctorName;
        }

        private string GetAppointmentTypeName(int id)
        {
            AppointmentsTypes appointmentType = db.AppointmentsTypes.Find(id);
            if (appointmentType == null)
            {
                return null;
            }
            return appointmentType.name;
        }

        // PUT: api/Appointments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointments(int id, AppointmentCustom appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.idAppointment)
            {
                return BadRequest();
            }

            // Validate if the patient already has appointments assigned for today
            //Appointments patient = db.Appointments.Where(p => p.fk_idPatient == appointment.idPatient
            //&& p.AppointmentDateTime.Year == appointment.AppointmentDateTime.Year
            //&& p.AppointmentDateTime.Month == appointment.AppointmentDateTime.Month
            //&& p.AppointmentDateTime.Day == appointment.AppointmentDateTime.Day).FirstOrDefault();
            //if (patient != null)
            //    return NotFound();

            int typeId = db.AppointmentsTypes.Where(at => at.name.ToLower() == appointment.appointmentType.ToLower()).FirstOrDefault().idAppointmentType;
            int doctorId = db.Doctors.Where(at => at.doctorName.ToLower() == appointment.doctorName.ToLower()).FirstOrDefault().idDoctor;

            Appointments app = new Appointments {
                idAppointment = appointment.idAppointment,
                fk_idPatient = appointment.idPatient,
                fk_idDoctor = doctorId,
                fk_idAppointmentType = typeId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                isActive = true
            };

            db.Entry(app).State = EntityState.Modified;

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
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // Validate if the patient already has appointments assigned for today
            Appointments patient = db.Appointments.Where(p => p.fk_idPatient == appointment.idPatient
            && p.AppointmentDateTime.Year == appointment.AppointmentDateTime.Year
            && p.AppointmentDateTime.Month == appointment.AppointmentDateTime.Month
            && p.AppointmentDateTime.Day == appointment.AppointmentDateTime.Day).FirstOrDefault();
            if (patient != null)            
                return NotFound();            

            // Get type and doctors ID
            int typeId = db.AppointmentsTypes.Where(at => at.name.ToLower() == appointment.appointmentType.ToLower()).FirstOrDefault().idAppointmentType;
            int doctorId = db.Doctors.Where(at => at.doctorName.ToLower() == appointment.doctorName.ToLower()).FirstOrDefault().idDoctor;

            Appointments _appointment = new Appointments {
                fk_idPatient = appointment.idPatient,
                fk_idDoctor = doctorId,
                fk_idAppointmentType = typeId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                isActive = true
            };

            db.Appointments.Add(_appointment);
            db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = _appointment.idAppointment }, _appointment);
            return Ok();
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

            // Get hours between dates
            DateTime now = DateTime.Now;
            double hours = (appointments.AppointmentDateTime - now).TotalHours;
            if (hours < 24)
            {
                return BadRequest();
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