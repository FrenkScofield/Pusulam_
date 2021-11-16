using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace PusulamBusiness.Mobile
{
    public class MMorpa : DBase
    {
        public string MorpaLink(JObject j)
        {
            dynamic jObj = j;

            using (var client = new WebClient())
            {
                var postData = "at=ac";
                postData += "&uyeip=" + jObj.IP;
                //95.6.79.122
                //postData += "&uyeip=95.6.79.122";
                postData += "&tckimlik=" + jObj.TCKIMLIKNO;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var responseString = client.DownloadString("https://www.morpakampus.com/api.asp?" + postData);
                var truefalse = XElement.Parse(responseString).Descendants("OK").Single().Value;


                if (truefalse == "1")
                {
                    var autcode = XElement.Parse(responseString).Descendants("R").Single().Attribute("authcode").Value;
                    var domain = XElement.Parse(responseString).Descendants("R").Single().Attribute("domain").Value;
                    return "https://" + domain + "/api.asp?at=giris&ac=" + autcode;
                }
                else
                {
                    var hata = XElement.Parse(responseString).Descendants("HATA").Single().Value;
                    return hata;
                }
            }
        }
    }
}
