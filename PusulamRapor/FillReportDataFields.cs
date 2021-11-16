using System;
using System.Data;
using DevExpress.XtraReports.UI;

namespace PusulamRapor
{
    public class FillReportDataFields
    {
        public static void Fill(Band b, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (XRControl x in b.Controls)
                    {
                        if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Text", dt, String.Format("{0}.{1}", dt.TableName, x.Text));
                        }
                        else if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && x.Tag.ToString() == "2")
                        {
                            x.DataBindings.Add("Text", dt, String.Format("{0}.{1}", dt.TableName, x.Text), "{0:0.00}");
                        }
                        else if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRRichText" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Html", dt, String.Format("{0}.{1}", dt.TableName, x.Text));
                        }
                        else if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && x.Tag.ToString() == "-1")
                        {
                            x.DataBindings.Add("Text", dt, String.Format("{0}.{1}", dt.TableName, x.Text), "{0:#.000}");
                        }
                        else if (true)
                        {

                        }
                    }
                }
                else
                {
                    foreach (XRControl x in b.Controls)
                    {
                        if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Text", null, "-");
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static void FillPanel(Band b, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (XRControl x in b.Controls)
                    {
                        if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRPanel" && x.Tag.ToString() == "1")
                        {
                            foreach (XRControl y in x.Controls)
                            {
                                if (y.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && y.Tag.ToString() == "1")
                                {
                                    y.DataBindings.Add("Text", dt, String.Format("{0}.{1}", dt.TableName, y.Text));
                                }
                                else if (y.GetType().ToString() == "DevExpress.XtraReports.UI.XRRichText" && y.Tag.ToString() == "1")
                                {
                                    y.DataBindings.Add("Html", dt, String.Format("{0}.{1}", dt.TableName, y.Text));
                                }
                            }
                        }
                        else if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Text", dt, String.Format("{0}.{1}", dt.TableName, x.Text));
                        }
                        else if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRRichText" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Html", dt, String.Format("{0}.{1}", dt.TableName, x.Text));
                        }
                    }
                }
                else
                {
                    foreach (XRControl x in b.Controls)
                    {
                        if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Text", null, "-");
                        }
                        else if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRRichText" && x.Tag.ToString() == "1")
                        {
                            x.DataBindings.Add("Html", dt, String.Format("{0}.{1}", dt.TableName, x.Text));
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
    }

}
