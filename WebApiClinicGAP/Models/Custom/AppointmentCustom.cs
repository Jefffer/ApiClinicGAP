using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiClinicGAP.Models.Custom
{
    public class AppointmentCustom
    {
        public int idAppointment { get; set; }
        public int idPatient { get; set; }
        public string patientName { get; set; }
        public string appointmentType { get; set; }
        public string doctorName { get; set; }
        public System.DateTime AppointmentDateTime { get; set; }
    }
}