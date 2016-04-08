using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;



namespace AlphaOne.Models
{
    public interface ICommodity
    {
        IList<Commodity> FindAll();
         int ID { get; set; }
         float Price { get; set; }
         DateTime Date { get; set; }
         string Type { get; set; }
    }
    public class Commodity
    {
        public int ID { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

    }

}