using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing.Imaging;
using System.Text;
using System.Runtime.Serialization;

namespace FingerprintPayment.Class
{
    public static class ExtensionMethod
    {
        public static T? ToNumber<T>(this string input) where T : struct
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            try
            {
                var convertedValue = (T)Convert.ChangeType(input, typeof(T));
                return convertedValue;
            }
            catch
            {
                return null;
            }
        }

        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return string.Format("{0} …", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        public static DateTime? ToEnCulture(this string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime dt = DateTime.ParseExact(date, "d/M/yyyy", new CultureInfo("th-Th"));

                return dt;
            }
            return null;
        }

        public static string NullIfWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static void SetCellRange(this ExcelRange xrange
            , bool isHeader = false
            , bool isMerge = false
            , string text = ""
            , int fontSize = 11
            , bool isBold = false
            , bool isWrap = true
            , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
            , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
            , Color? color = null
            , Color? bgColor = null
            , ExcelBorderStyle border = ExcelBorderStyle.None)
        {
            using (xrange)
            {
                xrange.Merge = isMerge;
                xrange.Value = text;
                xrange.Style.Font.Bold = isBold;
                xrange.Style.HorizontalAlignment = horizotal;
                xrange.Style.VerticalAlignment = vetical;
                xrange.Style.Font.Size = fontSize;
                xrange.Style.WrapText = isWrap;
                xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;

                if (bgColor.HasValue)
                {
                    xrange.Style.Fill.BackgroundColor.SetColor(bgColor.Value);
                }
                else
                {
                    xrange.Style.Fill.BackgroundColor.SetColor(0, 255, 255, 255);
                }

                if (isHeader)
                {
                    xrange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    xrange.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    xrange.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
                }



                if (color.HasValue)
                {
                    xrange.Style.Font.Color.SetColor(color.Value);
                }

                if (border != ExcelBorderStyle.None)
                {
                    xrange.Style.Border.Left.Style = border;
                    xrange.Style.Border.Right.Style = border;
                    xrange.Style.Border.Bottom.Style = border;
                    xrange.Style.Border.Top.Style = border;
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                }

                xrange.AutoFitColumns();

            }
        }

        public static PdfPCell SetCellPDF(this PdfPCell cell
            , iTextSharp.text.Font _font
            , string text = ""
            , int fontSize = 14
            , int fontStyle = iTextSharp.text.Font.NORMAL
            , int horizotal = Element.ALIGN_CENTER
            , int vetical = Element.ALIGN_MIDDLE
            , int border = iTextSharp.text.Rectangle.BOX
            , int colspan = 1
            , int rowspan = 1
            , Color? bgColor = null
            , int paddingTop = 2
            , int paddingBottom = 2
            , int paddingLeft = 2
            , int paddingRight = 2
            )
        {
            //var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize, fontStyle);
            var font = new iTextSharp.text.Font(_font.BaseFont, fontSize, fontStyle);
            //font.Size = fontSize;
            //font.SetStyle(fontStyle);

            if (!string.IsNullOrEmpty(text))
            {
                var p = new Phrase(text, font);
                cell.Phrase = p;
            }

            //var cell = new PdfPCell(p);
            //cell.UseAscender = true;
            cell.VerticalAlignment = vetical;
            cell.HorizontalAlignment = horizotal;
            cell.Border = border;
            cell.PaddingBottom = paddingBottom;
            cell.PaddingTop = paddingTop;
            cell.PaddingLeft = paddingLeft;
            cell.PaddingRight = paddingRight;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            if (bgColor.HasValue)
            {
                cell.BackgroundColor = new BaseColor(bgColor.Value);
            }
            return cell;
        }

        /// <summary>  
        /// Some of the EXIF values for setting. To expand use complete list of EXIF values
        /// </summary>  
        public enum MetaProperty
        {
            Title = 40091, //Byte
            Comment = 40092, //Byte
            Author = 40093, //Byte
            Keywords = 40094, //Byte
            Subject = 40095, //Byte
            Copyright = 33432, //Ascii
            Software = 11, //Rational
            DateTime = 36867, //Ascii
            UserComment = 37510, //Comment
            ImageDescription = 270, //Ascii
            ProfileName = 50936, //Byte
            ImageID = 32781 //Ascii
        }
        public static Bitmap SetMetaValue(this Bitmap sourceBitmap, MetaProperty property, string value)
        {
            PropertyItem prop = sourceBitmap.PropertyItems[0];
            int iLen = value.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)value[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = (int)property;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
            sourceBitmap.SetPropertyItem(prop);
            return sourceBitmap;
        }
        public static string GetMetaValue(this Bitmap sourceBitmap, MetaProperty property)
        {
            PropertyItem[] propItems = sourceBitmap.PropertyItems;
            var prop = propItems.FirstOrDefault(p => p.Id == (int)property);
            if (prop != null)
            {
                return Encoding.UTF8.GetString(prop.Value);
            }
            else
            {
                return null;
            }
        }

    }
}