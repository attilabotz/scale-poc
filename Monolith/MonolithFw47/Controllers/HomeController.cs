using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MonolithFw47.Models;

namespace MonolithFw47.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMessage(MessageViewModel messageView)
        {
            if (!this.ModelState.IsValid)
            {
                return View("Index");
            }

            return View("Index");
        }
    }
}