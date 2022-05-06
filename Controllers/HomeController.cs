using System.Net;
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
using System.Web.Script.Serialization;

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
        
        [Authorize (Roles ="Seller")]
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
        public ActionResult addSignup(Tbl_User model, HttpPostedFileBase file, FormCollection objfrm)
        {

            Tbl_User obj1 = new Tbl_User();
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProfileImages/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            obj1.Email = model.Email;
            obj1.Username = model.Username;
            obj1.password = model.password;
            obj1.location = model.location;
            
            model.Profilepic = file != null ? pic : model.Profilepic;
            obj1.Profilepic = model.Profilepic;
            
            ctx.Tbl_User.Add(obj1);
            ctx.SaveChanges();
            if (model.id != 0)
            {
                string data1 = objfrm["menu"];
                Tbl_Roles tt = new Tbl_Roles();
                tt.RoleName = data1;
                tt.UserId = model.id;
                ctx.Tbl_Roles.Add(tt);
                ctx.SaveChanges();
                return RedirectToAction("Login");

            }
            else
            {
                return RedirectToAction("SelectRole");
            }

        }
        public ActionResult SelectRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SelectRole(int role, string name)
        {
            Tbl_Roles tt = new Tbl_Roles();
            tt.UserId = role;
            tt.RoleName = name;
            ctx.Tbl_Roles.Add(tt);
            ctx.SaveChanges();
            return RedirectToAction("Login");
 
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_User model1,String returnUrl)
        {
         
            Tbl_User obj1 = ctx.Tbl_User.Where(x => x.Username.Equals(model1.Username) && x.password.Equals(model1.password)).First();
            if (obj1 != null)
            {
                FormsAuthentication.SetAuthCookie(obj1.Username, false);
                if (Url.IsLocalUrl(returnUrl)&&returnUrl.Length>1&&returnUrl.StartsWith("/")&& !returnUrl.StartsWith("//")&& !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.error = "Invalid username and password.";
                return View();
            }

        }

        public ActionResult sendRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sendRequest(int serviceid, Tbl_Orders order)
        {

            String sellername="";
            foreach(var item in ctx.Tbl_Service.ToList())
            {
                if(item.ServiceId== serviceid)
                {
                    sellername = item.Username;
                }
            }


            Tbl_Orders tbl = new Tbl_Orders();
            tbl.description = order.description;
            tbl.CustomerName = User.Identity.Name;
            tbl.SellerName = sellername;
            tbl.Status = "Pending";
            tbl.Date = DateTime.Now;
            tbl.Address = order.Address;
            tbl.Phone_number = order.Phone_number;
            ctx.Tbl_Orders.Add(tbl);
            ctx.SaveChanges();
            return View();
        }

        public ActionResult Logout()
        {


            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Home");
        }
        public ActionResult Myorders()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
        }
        public ActionResult DeleteOrder(int id)
        {
            var res = ctx.Tbl_Orders.Where(x => x.id == id).First();
            ctx.Tbl_Orders.Remove(res);
            ctx.SaveChanges();
            return RedirectToAction("Myorders");
        }
        public ActionResult Loc()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            ViewBag.IPAddress = ipAddress;

            return View();
        }
       

        public ActionResult CustomerOrder()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
        }
        public ActionResult CancelOrder(int id)
        {
            Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            obj.Status = "Denied";

            _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            return RedirectToAction("CustomerOrder");

        }
        public ActionResult ReviewByCustomer(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReviewByCustomer(Tbl_review model, int id)
        {
            Tbl_review obj = new Tbl_review();
            obj.Orderid = id;
            obj.date = DateTime.Now;
            obj.rating = model.rating;
            obj.Review = model.Review;
            String reviewrname = "";
            String reviewedname = "";
            foreach (var item in ctx.Tbl_Orders)
            {
                if (item.id == id)
                {

                     reviewedname = item.SellerName;
                    reviewrname = item.CustomerName;

                }
            }
            obj.reviwername = reviewrname;
            obj.reviewname = reviewedname;
            ctx.Tbl_review.Add(obj);
            ctx.SaveChanges();
            return View();
        }

        public ActionResult Ordercompleted()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
        }

    }
}