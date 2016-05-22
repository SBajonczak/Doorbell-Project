using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Data.SqlClient;
using Doorbell.Web.Models;

namespace Doorbell.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Rings> rings = new List<Rings>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
               rings = context.RingList.OrderByDescending(_=>_.timestamp).ToList();
            }

            return View(rings);
        }


        

        public IActionResult About()
        {
            ViewData["Message"] = "233223Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
