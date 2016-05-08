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

            //use HAP to get oil price
            HtmlWeb webget = new HtmlWeb();
            HtmlDocument document = webget.Load("http://finance.yahoo.com/q?s=clk16.nym");
            string price = document.DocumentNode.SelectSingleNode("id('yfs_l10_clk16.nym')").InnerHtml;
            //make oil object
            Commodity oil = new Commodity();
            oil.Date = DateTime.Now;
            oil.Price = float.Parse(price);
            oil.Type = "Oil";
            repository.Insert(oil);
            
            //db.Commodities.Add(oil);
            //db.SaveChanges();
            System.Threading.Thread.Sleep(100);

            //HAP to get soy
            webget = new HtmlWeb();
            document = webget.Load("http://finance.yahoo.com/futures?t=grains");
            price = document.DocumentNode.SelectSingleNode("id('yfs_l10_ck16.cbt')").InnerHtml;
            //make soy object
            Commodity corn = new Commodity();
            corn.Price = float.Parse(price);
            corn.Type = "Corn";
            corn.Date = DateTime.Now;
            repository.Insert(corn);
            //db.Commodities.Add(corn);
            //db.SaveChanges();

            //HAP to get rupee
            webget = new HtmlWeb();
            document = webget.Load("http://finance.yahoo.com/q?s=USDRUB=X");
            price = document.DocumentNode.SelectSingleNode("id('yfs_l10_usdrub=x')").InnerHtml;
            //make soy object
            Commodity rupee = new Commodity();
            rupee.Price = float.Parse(price);
            rupee.Type = "Rupee";
            rupee.Date = DateTime.Now;
            repository.Insert(rupee);
            repository.Save();
            //db.Commodities.Add(rupee);
            //db.SaveChanges();

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

        

        // GET: Commodities/Details/5
        public ActionResult Details(int? id)
        {
            
            var com = db.Commodities.Where(x => x.Type == "Oil").ToList();
            ArrayList prices = new ArrayList();
            ArrayList dates = new ArrayList();
            foreach(var Commodity in com)
            {
                prices.Add(Commodity.Price);
                dates.Add(Commodity.Date);
            }
            ViewBag.OilPrices = prices;
            ViewBag.OilDates = dates;
            return View();
        }

        // GET: Commodities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commodities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Type,Date")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Commodities.Add(commodity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commodity);
        }

        // GET: Commodities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodities.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: Commodities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Type,Date")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commodity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commodity);
        }

        // GET: Commodities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodities.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: Commodities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commodity commodity = db.Commodities.Find(id);
            db.Commodities.Remove(commodity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
