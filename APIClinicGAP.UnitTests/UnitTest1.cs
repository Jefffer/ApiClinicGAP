using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiClinicGAP.Controllers;
using WebApiClinicGAP.Models;

namespace APIClinicGAP.UnitTests
{
    [TestClass]
    public class AppointmentTests
    {
        [TestMethod]
        public void PostAppointment_CorrectAppointment_ReturnsTrue()
        {
            // Arrange
            var appointmentController = new AppointmentsController();

            // Act
            //var result = appointmentController.PostAppointments(new Appointments
            //{
            //    fk_idPatient = 1,
            //    fk_idAppointmentType = 1,
            //    fk_idDoctor = 1,
            //    isActive = true,
            //    AppointmentDateTime = DateTime.Now
            //});

        }
    }
}
