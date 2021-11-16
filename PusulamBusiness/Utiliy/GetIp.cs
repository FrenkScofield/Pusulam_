using System;

namespace PusulamBusiness.Utiliy
{
    public class GetIp
    {
        public string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;

            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (String.IsNullOrEmpty(VisitorsIPAddr))
            {
                VisitorsIPAddr = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;

        }
    }
}
