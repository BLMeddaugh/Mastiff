using HtmlAgilityPack;
using System;
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
    public class CommoditiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            db.Commodities.Add(oil);
            db.SaveChanges();

            //HAP to get soy
            webget = new HtmlWeb();
            document = webget.Load("http://finance.yahoo.com/futures?t=grains");
            price = document.DocumentNode.SelectSingleNode("id('yfs_l10_ck16.cbt')").InnerHtml;
            //make soy object
            Commodity corn = new Commodity();
            corn.Price = float.Parse(price);
            corn.Type = "Corn";
            corn.Date = DateTime.Now;
            db.Commodities.Add(corn);
            db.SaveChanges();
            //  return RedirectToAction("Index");
            return View(db.Commodities.ToList());
        }

        

        // GET: Commodities/Details/5
        public ActionResult Details(int? id)
        {
            
            var com = db.Commodities.Where(x => x.Type == "Oil").ToList();
            ViewBag.com = db.Commodities.Where(x => x.Type == "Oil").ToList();
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
