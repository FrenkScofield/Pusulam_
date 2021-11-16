<%@ WebHandler Language="C#" Class="Upload" %>
using System;
using System.Web;
public class Upload : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile uploads = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        string file = System.IO.Path.GetFileName(uploads.FileName);
        string filename = Guid.NewGuid() + "." + file.Substring(file.IndexOf('.') + 1);
        string path = context.Server.MapPath(".") + "\\Dosyalar\\CkResim\\"+ filename;
        uploads.SaveAs(path);// path of folder where images are upload
        string url = "/Dosyalar/CkResim/" + filename; // path of folder where images are upload
        context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        context.Response.End();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}