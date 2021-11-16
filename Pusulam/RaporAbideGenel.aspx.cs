using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.IO;
using System.Reflection;
using System.Web;

namespace Pusulam
{
    public partial class RaporAbideGenel : System.Web.UI.Page
    {
        string Oturum = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string rpr = Request.QueryString["rapor"].ToString();
                string p = Request.QueryString["p"].ToString();


                object[] o = new object[p.Split(';').Length];
                for (int i = 0; i < p.Split(';').Length; i++)
                {
                    o[i] = p.Split(';')[i];
                }

                Oturum = o[1].ToString();

                Type type = null;
                type = Assembly.Load("PusulamRapor").GetType(String.Format("PusulamRapor.{0}", rpr));
                XtraReport rapor = Activator.CreateInstance(type, o) as XtraReport;
                //ReportViewer1.Report = rapor;

                if (Request.QueryString["raporTur"] == null || Request.QueryString["raporTur"] == "HTML")
                {
                    ReportViewer1.Report = rapor;
                }
                else
                {
                    string ciktiTuru = Request.QueryString["raporTur"].ToString();

                    string cType = string.Empty;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        switch (ciktiTuru)
                        {
                            case "PDF":
                                rapor.ExportToPdf(stream, new PdfExportOptions() { ImageQuality = PdfJpegImageQuality.Highest });
                                break;
                            case "XLS":
                                rapor.ExportToXls(stream, new XlsExportOptions());
                                break;
                            case "XLSX":
                                rapor.ExportToXlsx(stream, new XlsxExportOptions());
                                break;
                            case "RTF":
                                rapor.ExportToRtf(stream, new RtfExportOptions());
                                break;
                            case "HTML":
                                rapor.ExportToHtml(stream, new HtmlExportOptions());
                                break;
                            case "TEXT":
                                rapor.ExportToText(stream, new TextExportOptions());
                                break;
                            default:
                                rapor.ExportToPdf(stream, new PdfExportOptions());
                                break;
                        }

                        if (ciktiTuru.ToLower() == "xls" || ciktiTuru.ToLower() == "xlsx")
                            cType = "vnd.ms-excel";
                        else if (ciktiTuru.ToLower() == "pdf")
                            cType = "pdf";
                        else if (ciktiTuru.ToLower() == "rtf")
                            cType = "rtf";
                        else
                            cType = "pdf";


                        stream.Seek(0, SeekOrigin.Begin);

                        stream.Flush(); //Always catches me out
                        stream.Position = 0; //Not sure if this is required

                        var bytes = stream.ToArray();

                        Response.ContentType = "application/" + cType;

                        if (ciktiTuru.ToLower() == "xls" || ciktiTuru.ToLower() == "xlsx" || ciktiTuru.ToLower() == "rtf")
                        {
                            string attachment = "attachment; filename=Rapor." + ciktiTuru.ToLower();
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);

                            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
                            Response.Charset = "windows-1254";
                        }


                        Response.Buffer = true;
                        Response.Clear();
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.OutputStream.Flush();

                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition",
                                           "attachment; filename=" + Oturum + ".zip;");
                        response.BinaryWrite(File.ReadAllBytes(Server.MapPath("/Dosyalar/AbideRapor/" + Oturum + ".zip")));
                        response.Flush();

                        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                        HttpContext.Current.Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}