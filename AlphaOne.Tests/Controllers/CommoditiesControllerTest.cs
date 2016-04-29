using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlAgilityPack;
using AlphaOne.Models;

namespace AlphaOne.Tests.Controllers
{
    [TestClass]
    public class CommoditiesControllerTest
    {
        Commodity commodity = new Commodity();
        [TestMethod]
        public void GrabbingAndApplyingPrices()
        {
            HtmlWeb webget = new HtmlWeb();
            HtmlDocument document = webget.Load("http://finance.yahoo.com/q?s=clk16.nym");
            string price = document.DocumentNode.SelectSingleNode("id('yfs_l10_clk16.nym')").InnerHtml; 
            commodity.Price = float.Parse(price);
            Assert.IsNotNull(commodity.Price);
        }
        [TestMethod]
        public void CorrectType()
        {
            string type = "Crude Oil";
            commodity.Type = type;
            Assert.Equals(commodity.Type, "Crude Oil");
        }
        [TestMethod]
        public void DateBeingSet()
        {
           var curDate = DateTime.Now;
            commodity.Date = curDate;
            Assert.AreEqual(commodity.Date, curDate);
        }
    }
}
