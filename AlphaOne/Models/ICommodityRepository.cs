using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlphaOne.Models;

namespace AlphaOne.Models
{
    public interface ICommodityRepository
    {
        IEnumerable<Commodity> SelectAll();
        Commodity SelectByID(int? id);
        void Insert(Commodity obj);
        void Update(Commodity obj);
        void Delete(int id);
        void Save();
        void Dispose();
    }
}