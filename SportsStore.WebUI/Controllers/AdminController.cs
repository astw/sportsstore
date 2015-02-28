using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Edmx;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult UserInfo()
        {
            string userName = User.Identity.Name;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dateStamp = HttpContext.Timestamp;
            // AuditRequest(userName, serverName, clientIP, dateStamp, "Renaming product");
            // Retrieve posted data from Request.Form 
            
            return View("ProductRenamed"); 
             
        }

        public ActionResult AjaxTest()
        {
            return View();
        }

        public ViewResult Index()
        {

            ViewBag.date = DateTime.Now;
            ViewBag.Message = "welcome to ASP.NET mvc"; 
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductEntity product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values 
                return View(product);
            }
        } 
    }
}
