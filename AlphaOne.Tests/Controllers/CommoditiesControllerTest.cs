using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlAgilityPack;
using AlphaOne.Models;

namespace AlphaOne.Tests.Controllers
{
    [TestClass]
    public class CommoditiesControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            HtmlWeb webget = new HtmlWeb();
            HtmlDocument document = webget.Load("http://finance.yahoo.com/q?s=clk16.nym");
            string price = document.DocumentNode.SelectSingleNode("id('yfs_l10_clk16.nym')").InnerHtml;
            string type = "Crude Oil";

            Commodity commodity = new Commodity();
            commodity.Price = float.Parse(price);
            Assert.IsNotNull(commodity.Price);
        }
    }
}
