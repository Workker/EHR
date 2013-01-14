using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    public class PatientController : Controller
    {
        #region Views

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region PartialViews

        public PartialViewResult Antimicrobial()
        {
            return PartialView("_Antimicrobial");
        }

        public PartialViewResult Charts()
        {
            return PartialView("_Charts");
        }

        public PartialViewResult ColonizationbyMdr()
        {
            return PartialView("_ColonizationbyMDR");
        }

        public PartialViewResult DataHigh()
        {
            return PartialView("_DataHigh");
        }

        public PartialViewResult Exams()
        {
            return PartialView("_Exams");
        }

        public PartialViewResult Form()
        {
            return PartialView("_Form");
        }

        public PartialViewResult GeneralData()
        {
            return PartialView("_GeneralData");
        }

        public PartialViewResult Hemotransfusion()
        {
            return PartialView("_hemotransfusion");
        }

        public PartialViewResult Images()
        {
            return PartialView("_Images");
        }

        public PartialViewResult OtherMedicationsRelevant()
        {
            return PartialView("_OtherMedicationsRelevant");
        }

        public PartialViewResult Prescriptions()
        {
            return PartialView("_Prescriptions");
        }

        public PartialViewResult Procedures()
        {
            return PartialView("_Procedures");
        }

        #endregion

        public string Admission(string q)
        {
            string stringReturn = null;

            if (q.Equals("c") || q.Equals("C"))
            {
                stringReturn = "[{\"name\":\"Clínica\",\"id\":\"1\"}, {\"name\":\"Cirúrgica\",\"id\":\"2\"}]";
            }
            else if (q.Equals("e") || q.Equals("E"))
            {
                stringReturn = "[{\"name\":\"Eletiva\",\"id\":\"3\"}, {\"name\":\"Emergência\",\"id\":\"4\"}]";
            }
            return stringReturn;
        }

        public string SearchPeaple(string query)
        {
            return
                "{\"results\":[{\"type\":\"header\",\"text\":\"People\"},{\"type\":\"person\",\"name\":\"Andrew Brookes\", \"imageUrl\":\"../Images/Profiles/1.jpg\"},{\"type\":\"person\",\"name\":\"Leo Coates\", \"imageUrl\":\"../Images/Profiles/2.jpg\"},{\"type\":\"person\",\"name\":\"Reece Davison\", \"imageUrl\":\"../Images/Profiles/3.jpg\"},{\"type\":\"person\",\"name\":\"Lauren Stanley\", \"imageUrl\":\"../Images/Profiles/4.jpg\"},{\"type\":\"person\",\"name\":\"Courtney Herbert\", \"imageUrl\":\"../Images/Profiles/5.jpg\"},{\"type\":\"person\",\"name\":\"Emily Holden\", \"imageUrl\":\"../Images/Profiles/6.jpg\"},{\"type\":\"person\",\"name\":\"Dylan Lynch\", \"imageUrl\":\"../Images/Profiles/7.jpg\"}]}";
        }

        public PartialViewResult DiagnosticForm()
        {
            return PartialView("GeneralData/_DiagnosticsForm");
        }

        public void DeleteDiagnostic()
        {
            
        }

    }
}
