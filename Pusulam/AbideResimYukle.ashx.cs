using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Pusulam
{
    public class AbideResimYukle : IHttpHandler
    {
        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;

            string DosyaAd = context.Request["filename"];
            string yol = "~/Dosyalar/AbideResim/" + context.Request["ID_ABIDESINAV"] + "/" + context.Request["ID_ABIDESAYFATUR"];

            string extension = System.IO.Path.GetExtension(context.Request.Files[0].FileName).ToLower();

            if ((extension == ".jpg" || extension == ".jpeg" || extension == ".png"))
            {
                #region Dosya 
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = null;

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        file = context.Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            var path = Path.Combine(Path.Combine(context.Server.MapPath(yol + "/"), DosyaAd + ".png"));
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            if (!Directory.Exists(context.Server.MapPath(yol + "/")))
                            {
                                Directory.CreateDirectory(context.Server.MapPath(yol + "/"));
                            }
                            file.SaveAs(path);
                        }
                    }
                }
                #endregion
            }
            else
            {
                context.Response.Write("Yalnızca jpg ve png dosyaları yükleyebilirsiniz.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}