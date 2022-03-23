using OnlineHomeServices.DAL;
using OnlineHomeServices.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace OnlineHomeServices.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbOnlineHomeServicesEntities context = new dbOnlineHomeServicesEntities();
        public IPagedList<Tbl_Service> ListOfServices { get; set; }
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {

            SqlParameter[] param = new SqlParameter[] {
                   new SqlParameter("@search",search??(object)DBNull.Value)
                   };
            IPagedList<Tbl_Service> data = context.Database.SqlQuery<Tbl_Service>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListOfServices = data
            };
        }
    }
}
