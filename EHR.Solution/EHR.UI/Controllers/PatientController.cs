using System;
using EHR.Controller;
using EHR.CoreShared.Entities;
using EHR.UI.Filters;
using System.Web.Mvc;
using EHR.UI.Infrastructure.Notification;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class PatientController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registering(string name, string cpf, DateTime Birthday, char genre, string identity)
        {
            try
            {
                var patient = new Patient
                    {
                        Name = name,
                        CPF = cpf,
                        DateBirthday = Birthday,
                        Genre = genre,
                        Identity = identity
                    };

                FactoryController.GetController(ControllerEnum.Patient).Registering(patient);

                this.ShowMessage(MessageTypeEnum.Success, "Paciente cadastrado com sucesso.");

                return View("Index");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return View("Index");
            }
        }
    }
}