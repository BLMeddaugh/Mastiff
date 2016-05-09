using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AlphaOne.Models
{
    [Authorize]
    public class CommoditiesController : Controller
    {
        private ICommodityRepository repository = null;
        private ApplicationDbContext db = new ApplicationDbContext();

        public CommoditiesController()
        {
            this.repository = new CommodityRepository();
        }
        public CommoditiesController(ICommodityRepository repository)
        {
            this.repository = repository;
        }
        // GET: Commodities
        public ActionResult Index()
        {
            makeCommoditity("http://finance.yahoo.com/q?s=clk16.nym", "id('yfs_l10_clk16.nym')", "Oil");
            makeCommoditity("http://finance.yahoo.com/futures?t=grains", "id('yfs_l10_ck16.cbt')", "Corn");

            makeCommoditity("http://finance.yahoo.com/q?s=USDRUB=X", "id('yfs_l10_usdrub=x')", "RUP");
            makeCommoditity("http://finance.yahoo.com/q?s=USDBRL=X", "id('yfs_l10_usdbrl=x')", "BRL");


            //int latest = db.Commodities.LastOrDefault().ID;
            //int i = 1;
            //while (db.Commodities.LastOrDefault().Price == db.Commodities.ElementAt(latest - i).Price)
            //{
            //    i++;
            //}
            //if (db.Commodities.LastOrDefault().Price > db.Commodities.ElementAt(latest - i).Price)
            //    ViewBag.change = "Up";
            //else
            //    ViewBag.change = "Down";
            ////  return RedirectToAction("Index");
            return View(db.Commodities.ToList());
        }

        public void makeCommoditity(string site, string x, string type)
        {

            HtmlWeb webget = new HtmlWeb();
            HtmlDocument document = webget.Load(site);
            string price = document.DocumentNode.SelectSingleNode(x).InnerHtml;
            Commodity c = new Commodity();
            c.Date = DateTime.Now;
            c.Price = float.Parse(price);
            c.Type = type;
            repository.Insert(c);
            repository.Save();
        }

        // GET: Commodities/Details/5
        public ActionResult Details(int? id)
        {

            var com = db.Commodities.Where(x => x.Type == "Oil").ToList();
            ArrayList prices = new ArrayList();
            ArrayList dates = new ArrayList();
            foreach (var Commodity in com)
            {
                prices.Add(Commodity.Price);
                dates.Add(Commodity.Date);
            }
            ViewBag.OilPrices = prices;
            ViewBag.OilDates = dates;

            return View();
        }

        public ActionResult OilGraph()
        {
            var myChart = new Chart(width: 600, height: 400)
           .AddTitle("Price History")
           .AddSeries(
              chartType: "line",
               name: "Oil Price",
               xValue: ViewBag.OilDates,
               yValues: ViewBag.OilPrices)
           .GetBytes("png");
           return File(myChart, "image/bytes");
        }
    }
}