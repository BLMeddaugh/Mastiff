using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using AlphaOne;
using AlphaOne.Models;
using HtmlAgilityPack;


namespace AlphaOne.Tests.Controllers
{
    [TestFixture]
    class CommoditiesControllerTest
    {
        [TestCase]
        public void testCommodityPriceSetting()
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
