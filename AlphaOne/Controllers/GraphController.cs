using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AlphaOne.Controllers
{
    public class GraphController : Controller
    {
        // GET: Graph
        public ActionResult Graph()
        {
            var myChart = new Chart(width: 600, height: 400)
           .AddTitle("Price History")
           .AddSeries(
            name: "Price",
            xValue: ViewBag.OilDates,
            yValues: ViewBag.OilPrices)
            .Write()
            .GetBytes("png");

            return File(myChart, "image/png");
            
        }
    }
}