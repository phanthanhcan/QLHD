using HopDongMgr.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HopDongMgr.Class.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        // Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["userid"] != null)
            {
                HopDongMgrEntities db = new HopDongMgrEntities();
                var ts = db.HT_ThamSo.Where(s => s.ID == 1).FirstOrDefault();

                if (ts.GiaTri == 1)
                {
                    var userID = (Guid)HttpContext.Current.Session["userid"];

                    if (userID != null)
                    {
                        var idNhom = db.HT_NguoiDung.Where(c => c.oid == userID).Select(s => s.IdNhom).FirstOrDefault();

                        // Lấy thông tin đường dẫn đang yêu cầu
                        var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                        //var action = filterContext.ActionDescriptor.ActionName;
                        var action = ((System.Web.Mvc.ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.Name;
                        var attributes = String.Join(",", filterContext.ActionDescriptor.GetCustomAttributes(true).Select(a => a.GetType().Name.Replace("Attribute", "")));
                        var path = controller + "-" + action + "-" + attributes;
                        //---

                        // So sánh với thông tin phân quyền của user được sử dụng những chức năng nào
                        var idNhom_Parameter = new SqlParameter("@idNhom", idNhom);
                        var list = db.Database.SqlQuery<HT_DSChucNang>("GetInfoChucNangFromIdNhom @idNhom", idNhom_Parameter).ToList();
                        bool check = false;

                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].TenHienThi == path)
                            {
                                check = true;
                                break;
                            }
                        }

                        if (!check)
                        {
                            if (path.Contains("GetPhanQuyen") || path.Contains("Delete") || path.Contains("ThamTraKiemSoat"))
                            {
                                filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary(new { controller = "Home", action = "AuthorizationFailed", useLayout = false })
                                );
                            }
                            else
                            {
                                filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary(new { controller = "Home", action = "AuthorizationFailed", useLayout = true })
                                );
                            }
                        }
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary(new { controller = "Home", action = "Login" })
                        );
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                               new RouteValueDictionary(new { controller = "Home", action = "Login" })
                       );
            }
        }
    }
}