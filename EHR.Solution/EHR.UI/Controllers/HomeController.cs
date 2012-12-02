using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    public class HomeController : Controller
    {
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

        public string SearchPeaple(string query)
        {
            return
                "{\"results\":[{\"type\":\"header\",\"text\":\"People\"},{\"type\":\"person\",\"name\":\"Andrew Brookes\", \"imageUrl\":\"../Images/Profiles/1.jpg\"},{\"type\":\"person\",\"name\":\"Leo Coates\", \"imageUrl\":\"../Images/Profiles/2.jpg\"},{\"type\":\"person\",\"name\":\"Reece Davison\", \"imageUrl\":\"../Images/Profiles/3.jpg\"},{\"type\":\"person\",\"name\":\"Lauren Stanley\", \"imageUrl\":\"../Images/Profiles/4.jpg\"},{\"type\":\"person\",\"name\":\"Courtney Herbert\", \"imageUrl\":\"../Images/Profiles/5.jpg\"},{\"type\":\"person\",\"name\":\"Emily Holden\", \"imageUrl\":\"../Images/Profiles/6.jpg\"},{\"type\":\"person\",\"name\":\"Dylan Lynch\", \"imageUrl\":\"../Images/Profiles/7.jpg\"}]}";
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}