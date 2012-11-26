using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

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
    }
}