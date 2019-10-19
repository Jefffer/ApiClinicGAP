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
using ClinicGAPDataAcces.Repositories;

namespace WebApiClinicGAP.Controllers
{
    public class PatientsController : ApiController
    {
        //private DBModel db = new DBModel();
        PatientRepository repository = new PatientRepository();

        // GET: api/Patients
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Patients> GetPatients()
        {
            List<Patients> patients = repository.GetAll();
            //List<Patients> _patients = new List<Patients>();
            //foreach (var pat in patients)
            //{
            //    pat.Appointments;
            //    _patients.Add(pat);
            //}
            return patients;
        }

        // GET: api/Patients/5
        [ResponseType(typeof(Patients))]
        public IHttpActionResult GetPatients(int id)
        {
            //Patients patients = db.Patients.Find(id);
            Patients patients = repository.GetById(id);
            if (patients == null)
            {
                return NotFound();
            }

            return Ok(patients);
        }

        public string GetPatientName(int id)
        {
            Patients patients = repository.GetById(id);
            if (patients == null)
            {
                return null;
            }

            return patients.patientName;
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatients(int id, Patients patients)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != patients.idPatient)
            {
                return BadRequest();
            }

            try
            {
                //db.Entry(patients).State = EntityState.Modified;
                repository.Update(patients);
                //db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientsExists(id))
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

        // POST: api/Patients
        [ResponseType(typeof(Patients))]
        public IHttpActionResult PostPatients(Patients patients)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // Existing patient validation
            Patients _patient = repository.GetAll().Where(p => p.idPatient == patients.idPatient).FirstOrDefault();
            if (_patient != null)
            {
                return BadRequest();
            }

            repository.Add(patients);
            //db.Patients.Add(patients);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patients.idPatient }, patients);
        }

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patients))]
        public IHttpActionResult DeletePatients(int id)
        {
            Patients patients = repository.GetById(id);
            if (patients == null)
            {
                return NotFound();
            }

            repository.Delete(patients);
            //db.Patients.Remove(patients);
            //db.SaveChanges();

            return Ok(patients);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool PatientsExists(int id)
        {
            return repository.GetAll().Count(e => e.idPatient == id) > 0;
        }
    }
}