using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.IO;
using System.Reflection;

namespace Pusulam
{
    public partial class RaporPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string p = Request["p"].ToString();


                object[] o = new object[p.Split('@').Length];
                object[] o2 = new object[p.Split('@').Length - 2];
                for (int i = 0; i < p.Split('@').Length; i++)
                {
                    o[i] = p.Split('@')[i];
                    if (i > 1)
                    {
                        o2[i - 2] = p.Split('@')[i];
                    }
                }

                Type type = null;
                type = Assembly.Load("PusulamRapor").GetType(String.Format("PusulamRapor.{0}", o[0].ToString()));
                XtraReport rapor = Activator.CreateInstance(type, o2) as XtraReport;
                //ReportViewer1.Report = rapor;

                if (o[1].ToString() == null || o[1].ToString() == "HTML")
                {
                    ReportViewer1.Report = rapor;
                }
                else
                {
                    string ciktiTuru = o[1].ToString();

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