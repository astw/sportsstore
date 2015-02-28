using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            { 
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            ProductEntity prouduct = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (prouduct != null)
            {
                cart.AddItem(prouduct, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            ProductEntity product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
         
    }
}
