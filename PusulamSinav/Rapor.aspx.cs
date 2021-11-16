using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PusulamRapor.Upgrade;
using System;
using System.IO;
using System.Reflection;


namespace PusulamSinav
{
    public partial class Rapor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //base.OnLoad(e);
                string rpr = Request.QueryString["rapor"].ToString();
                string p = Request.QueryString["p"].ToString();


                object[] o = new object[p.Split(';').Length];
                for (int i = 0; i < p.Split(';').Length; i++)
                {
                    o[i] = p.Split(';')[i];
                }

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
                        switch (ciktiTuru.ToUpper())
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
                        Response.OutputStream.ReadTimeout = 0;
                        Response.OutputStream.Flush();
                        Response.End();
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