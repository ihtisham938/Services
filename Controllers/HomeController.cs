using Newtonsoft.Json;
using OnlineHomeServices.DAL;
using OnlineHomeServices.Models;
using OnlineHomeServices.Models.Home;
using OnlineHomeServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineHomeServices.Controllers
{
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbOnlineHomeServicesEntities ctx = new dbOnlineHomeServicesEntities();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }
        public ActionResult Checkout()
        {
            return View();
        }


        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int serviceId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var service = ctx.Tbl_Service.Find(serviceId);
                foreach (var item in cart)
                {
                    if (item.Service.ServiceId == serviceId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Service = service,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult AddToCart(int serviceId, string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var service = ctx.Tbl_Service.Find(serviceId);
                cart.Add(new Item()
                {
                    Service = service,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var service = ctx.Tbl_Service.Find(serviceId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Service.ServiceId == serviceId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Service = service,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Service.ServiceId == serviceId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Service = service,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect(url);
        }
        public ActionResult RemoveFromCart(int serviceId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Service.ServiceId == serviceId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }
        
        
        public ActionResult ServiceDetails(int serviceId)
        {
            var q = ctx.Tbl_Service.FirstOrDefault(m => m.ServiceId == serviceId);
            return View(q);

            //return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(serviceId));
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addSignup(Tbl_User model1)
        {
            Tbl_User obj1 = new Tbl_User();
            obj1.Email = model1.Email;
            obj1.Username = model1.Username;
            obj1.password = model1.password;
            obj1.location = model1.location;
            ctx.Tbl_User.Add(obj1);
            ctx.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_User model1)
        {
            Tbl_User obj1 = ctx.Tbl_User.Where(x => x.Username.Equals(model1.Username) && x.password.Equals(model1.password)).SingleOrDefault();
            if (obj1 != null)
            {
                Session["Name"] = obj1.Username.ToString();
                return Redirect("Index");

            }
            else
            {
                ViewBag.error = "Invalid username and password.";
                return View();
            }

        }

    }
}