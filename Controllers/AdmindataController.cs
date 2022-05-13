using OnlineHomeServices.DAL;
using OnlineHomeServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineHomeServices.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdmindataController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbOnlineHomeServicesEntities ctx = new dbOnlineHomeServicesEntities();
        // GET: Admindata
        public ActionResult Adminpro()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_User>().GetAllRecords());
        }
        public ActionResult Userdelete(int id)
        {

            var res1 = ctx.Tbl_Roles.Where(x => x.UserId == id).First();
            ctx.Tbl_Roles.Remove(res1);
            var res = ctx.Tbl_User.Where(x => x.id == id).First();
            ctx.Tbl_User.Remove(res);
            ctx.SaveChanges();
            return RedirectToAction("Adminpro");
        }
        public ActionResult Ban(int id)
        {
            Tbl_User obj = _unitOfWork.GetRepositoryInstance<Tbl_User>().GetFirstorDefault(id);
            obj.status = false;
            _unitOfWork.GetRepositoryInstance<Tbl_User>().Update(obj);
            return RedirectToAction("Adminpro");

        }
        public ActionResult banedUsers()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_User>().GetAllRecords());
        }
        public ActionResult Unban(int id)
        {
            Tbl_User obj = _unitOfWork.GetRepositoryInstance<Tbl_User>().GetFirstorDefault(id);
            obj.status = true;
            _unitOfWork.GetRepositoryInstance<Tbl_User>().Update(obj);
            return RedirectToAction("Adminpro");
        }
    }
}