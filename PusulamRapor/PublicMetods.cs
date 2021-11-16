using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusulamRapor
{
    public static class PublicMetods
    {
        public static DataTable orderBYtoTable(DataTable dt, string sort, string[] distinct = null)
        {
            try
            {
                DataView dw = dt.DefaultView;
                if (distinct != null)
                {
                    DataTable dt2= dw.ToTable(true, distinct);
                    dw = dt2.DefaultView;
                }
                dw.Sort = sort;
                return dw.ToTable();
            }
            catch (Exception ex)
            {
                return dt;
            }

        }
        public static XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(new FontFamily("Tahoma"), 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,

            };
            _LX += xrLabelAdd.WidthF;

            return xrLabelAdd;
        }

        public static XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor, Font _font)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = _font,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,

            };
            _LX += xrLabelAdd.WidthF;

            return xrLabelAdd;
        }

        public static XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor, string TAG)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(new FontFamily("Tahoma"), 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,
                Tag = TAG
            };
            _LX += xrLabelAdd.WidthF;

            return xrLabelAdd;
        }

        public static XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor, string TAG, Font _font)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = _font,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,
                Tag = TAG
            };
            _LX += xrLabelAdd.WidthF;

            return xrLabelAdd;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }



        public static XRLabel lblBaslik(string _text, float _LX, float _LY, float _lblEn, float _lblBoy)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = Color.FromArgb(76, 166, 213),
                //BackColor = Color.Transparent,
                ForeColor = Color.White,
                BorderColor = Color.White,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(new FontFamily("Tahoma"), 10, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,
            };
            _LX += xrLabelAdd.WidthF;

            return xrLabelAdd;
        }

        public static XRLabel lblDetay(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, string TAG)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(76, 166, 213),
                BorderColor = Color.FromArgb(76, 166, 213),
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(new FontFamily("Tahoma"), 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,
                Tag = TAG
            };
            _LX += xrLabelAdd.WidthF;

            return xrLabelAdd;
        }


        public static XRRichText lblDetayHtml(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, string TAG)
        {
            XRRichText xrRichTextAdd = new XRRichText()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(76, 166, 213),
                BorderColor = Color.FromArgb(76, 166, 213),
                BorderWidth = 1,
                WordWrap = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(new FontFamily("Tahoma"), 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanShrink = false,
                Tag = TAG
            };
            _LX += xrRichTextAdd.WidthF;

            xrRichTextAdd.ForeColor = Color.FromArgb(76, 166, 213);
            
            return xrRichTextAdd;
        }
    }
}
