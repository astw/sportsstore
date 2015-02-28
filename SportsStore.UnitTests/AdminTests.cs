using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new ProductEntity[]
            {
                new ProductEntity {ProductID = 1, Name = "P1"},
                new ProductEntity {ProductID = 2, Name = "P2"}
            });

            var target = new AdminController(mock.Object);

            var result = ((IEnumerable<ProductEntity>) target.Index().ViewData.Model).ToArray(); 

            Assert.AreEqual(result.Length,2);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
        }
    }
}
