using System;
using ClinicGAPDataAcces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiClinicGAP.Controllers;
using WebApiClinicGAP.Models;

namespace APIClinicGAP.UnitTests
{
    [TestClass]
    public class AppointmentTests
    {
        [TestMethod]
        public void PostAppointment_CorrectAppointment()
        {
            // Arrange
            var appointmentController = new AppointmentsController();
            var appointment = new Appointments
            {
                fk_idPatient = 1,
                fk_idAppointmentType = 1,
                fk_idDoctor = 1,
                isActive = true,
                AppointmentDateTime = DateTime.Now
            };

            // Act
            var result = appointmentController.PostAppointments(appointment);

            // Assert
            Appointments app;
            //Assert.IsTrue(result.TryGetContentValue<Appointments>(out app));
        }
    }
}
