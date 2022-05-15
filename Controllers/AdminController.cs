using Newtonsoft.Json;
using OnlineHomeServices.DAL;
using OnlineHomeServices.Models;
using OnlineHomeServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;



namespace OnlineHomeServices.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbOnlineHomeServicesEntities ctx = new dbOnlineHomeServicesEntities();
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
       
        public ActionResult Dashboard()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
        }


        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }
        public ActionResult Service()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Service>().GetService());
        }
        public ActionResult ServiceEdit(int serviceId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Service>().GetFirstorDefault(serviceId));
        }
        [HttpPost]
        public ActionResult ServiceEdit(Tbl_Service tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ServiceImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ServiceImage = file != null ? pic : tbl.ServiceImage;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Service>().Update(tbl);
            return RedirectToAction("Service");
        }

        public ActionResult counterOffer(int id)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id));
        }
        [HttpPost]
        public ActionResult CounterOffer(Tbl_Orders tbl, HttpPostedFileBase file)
        {
            tbl.Status = "counterPending";
            
            _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(tbl);
            return RedirectToAction("ShowRequest");
        }
        public ActionResult ServiceAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ServiceAdd(Tbl_Service tbl, HttpPostedFileBase file)
        {
            
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ServiceImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ServiceImage = pic;
            tbl.CreatedDate = DateTime.Now;
            tbl.Username = User.Identity.Name;
           _unitOfWork.GetRepositoryInstance<Tbl_Service>().Add(tbl);
            return RedirectToAction("Service");
        }
        public ActionResult serviceDelete(int serviceId)
        {
            var res = ctx.Tbl_Service.Where(x => x.ServiceId == serviceId).First();
            ctx.Tbl_Service.Remove(res);
            ctx.SaveChanges();
            return RedirectToAction("Service");
        }
        //User profile
        public ActionResult profile()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_User>().GetAllRecords());
        }

        [HttpPost]
        public ActionResult profile(Tbl_User tbl, HttpPostedFileBase file)
        {

            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ServiceImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.Profilepic = pic;
            _unitOfWork.GetRepositoryInstance<Tbl_User>().Add(tbl);
            return RedirectToAction("profile");
        }

        public ActionResult AcceptedOrder()
        {

          
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());

        }

        //Requests of orders

        public ActionResult ShowRequest()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
        }

        //public ActionResult AcceptOrder()
        //{ 
        //    return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
            
        //}
        //[HttpPost]
        public ActionResult AcceptOrder(int id)
        {
            
                
                Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            Tbl_Orders obj1 = new Tbl_Orders();
            obj1 = obj;
             
                obj.description = obj1.description;
                obj.SellerName = obj1.SellerName;
                obj.CustomerName = obj1.CustomerName;
                obj.Address = obj1.Address;
                obj.Long= obj1.Long;
                obj.Lat = obj1.Lat;
                obj.Phone_number = obj1.Phone_number;
                obj.orderprice = obj1.orderprice;
                obj.Date = obj1.Date;




                obj.Status = "Approved";
               
                _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
                ctx.SaveChanges();

                





            

            //Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            //obj.Status = "Approved";
            //_unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            return RedirectToAction("AcceptedOrder");

        }

        public ActionResult RejectOrder(int id)
        {
            Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);



            obj.Status = "Denied";

            _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            return RedirectToAction("ShowRequest");

        }


        public ActionResult OrdercompleteAdmin()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());

        }

        public ActionResult OrdercompleteAdmin1(int id)
          {
            //    Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            //    obj.Status = "Complete";
            //    _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            //return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());



            Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            Tbl_Orders obj1 = new Tbl_Orders();
            obj1 = obj;

            obj.description = obj1.description;
            obj.SellerName = obj1.SellerName;
            obj.CustomerName = obj1.CustomerName;
            obj.Address = obj1.Address;
            obj.Long = obj1.Long;
            obj.Lat = obj1.Lat;
            obj.Phone_number = obj1.Phone_number;
            obj.orderprice = obj1.orderprice;
            obj.Date = obj1.Date;




            obj.Status = "Complete";

            _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            ctx.SaveChanges();









            //Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            //obj.Status = "Approved";
            //_unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
           return RedirectToAction("AcceptedOrder");
        }

        public ActionResult Reviewsseller(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reviewsseller(Tbl_review model, int id)
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

                    reviewrname = item.SellerName;
                    reviewedname = item.CustomerName;

                }
            }
            obj.reviwername = reviewrname;
            obj.reviewname = reviewedname;
            ctx.Tbl_review.Add(obj);
            ctx.SaveChanges();
            return View();
        }
        [AllowAnonymous]
        public ActionResult Adminlogin()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Adminlogin(Tbl_User model1)
        {
            if (model1.Username == "Admin" && model1.password == "Admin")
            {
                return RedirectToAction("banedUsers", "Admindata");
            }
            else
            {
                ViewBag.error = "Invalid username or password.";
                return View();
            }

        }
        public ActionResult MyCounteroffers()
        {
           return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords());
        }
        public ActionResult MorderLocation(int id)
        {

            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecordsIQueryable().Where(i => i.id == id).First());

        }

        public ActionResult paymentseller(int id)
        {

            return View(_unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id));

        }



        public ActionResult paymentdone(int id)
        {


            Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            Tbl_Orders obj1 = new Tbl_Orders();
            obj1 = obj;

            obj.description = obj1.description;
            obj.SellerName = obj1.SellerName;
            obj.CustomerName = obj1.CustomerName;
            obj.Address = obj1.Address;
            obj.Long = obj1.Long;
            obj.Lat = obj1.Lat;
            obj.Phone_number = obj1.Phone_number;
            obj.orderprice = obj1.orderprice;
            obj.Date = obj1.Date;




            obj.Status = obj1.Status;
            obj.Paymentstatus = "paid";
            _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            ctx.SaveChanges();









            //Tbl_Orders obj = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetFirstorDefault(id);
            //obj.Status = "Approved";
            //_unitOfWork.GetRepositoryInstance<Tbl_Orders>().Update(obj);
            return RedirectToAction("paymentseller", new { id = obj.id });
        }

    }
}