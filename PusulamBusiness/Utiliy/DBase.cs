using System;
using System.Configuration;

namespace PusulamBusiness
{
    public class DBase
    {
        public int ID_MENU = 0;
        protected string conStr = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;

        public DBase()
        {
            bool isTest = ConfigurationManager.AppSettings["IsTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);

            if (isTest)
            {
                conStr = ConfigurationManager.ConnectionStrings["pusulamTest"].ConnectionString;
            }
        }


        public static string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            if (System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                VisitorsIPAddr = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
    }
}