using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlphaOne.Models;

namespace AlphaOne.Models
{
    public class CommodityRepository : ICommodityRepository
    {
        private ApplicationDbContext db = null;

        public CommodityRepository()
        {
            this.db = new ApplicationDbContext();
        }
        public CommodityRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Delete(string id)
        {
            Commodity exisiting = db.Commodities.Find(id);
            db.Commodities.Remove(exisiting);
        }

        public void Insert(Commodity obj)
        {
            db.Commodities.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Commodity> SelectAll()
        {
            return db.Commodities.ToList();
        }

        public Commodity SelectByID(string id)
        {
            return db.Commodities.Find(id);
        }

        public void Update(Commodity obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }
        public void Dispose()
        {
            db = null;
        }

        public Commodity SelectByID(int? id)
        {
            return db.Commodities.Find(id);
        }
        public void Delete(int id)
        {
            Commodity existing = db.Commodities.Find(id);
            db.Commodities.Remove(existing);
        }
    }
}
