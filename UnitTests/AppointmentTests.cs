using System;
using Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Logic;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class AppointmentTests
    {
        [TestMethod]
        public void Create_Appointment_Is_Called_Exactly_Once()
        {
            Mock<IAppointmentContext> mockContext = new Mock<IAppointmentContext>();
            Appointment appointment = new Mock<Appointment>(1, 3, 2, DateTime.Today, DateTime.Today, "Test").Object;
            mockContext.Setup(x => x.CreateAppointment(appointment));

            AppointmentLogic appointmentLogic = new AppointmentLogic(mockContext.Object);
            appointmentLogic.CreateAppointment(appointment);
            mockContext.Verify(x => x.CreateAppointment(appointment), Times.Exactly(1));
        }
    }
}