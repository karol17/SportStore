using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Abstract;
using SportsStore.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //przygotowanie
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1" },
                new Product {ProductID=2,Name="P2" },
                new Product {ProductID=3,Name="P3" },
                new Product {ProductID=4,Name="P4" },
                new Product {ProductID=5,Name="P5" }

            });

            ProductController controller=new ProductController(mock.Object);
            controller.PageSize = 3;
            //dzialanie
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            //asercje
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name,"P4");
            Assert.AreEqual(prodArray[1], "P5");

        }

        [TestMethod]
        public void Can_Generate_page_Links()
        {
            //przygotowanie-definiowanie metody pomocniczej HTMl- potrzebne aby użyć metody rozzerzającej
            HtmlHelper myHelper = null;

            //przygotowanie- tworzenie danych PagingInfo
            PagingInfo paginginfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            //przygotowanie-konfiguracja delegatu z użyciem wyrażenia lambda
            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            //dzialanie
            MvcHtmlString result = myHelper.PageLinks(paginginfo,pageUrlDelegate);

            //asercje
            Assert.AreEqual(@"<a class=""btn btn-defoult""href=""Strona1"">1</a>"
                    + @"<a class=""btn btn-defoult btn-primary selected"" href=""Strona2"">2</a>"
                    + @"<a class=""btn btn-defoult"" href=""Strona3"">3</a>", result.ToString());
        }
    }
}
