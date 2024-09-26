using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.StudentInfo.CsCode
{
    public class ITextEvents : PdfPageEventHelper
    {
        // Normal
        static BaseFont baseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        Font font8 = new Font(baseFont, 8);

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable pdfTable = new PdfPTable(1);
            //table.WidthPercentage = 100; //PdfPTable.writeselectedrows below didn't like this
            pdfTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]

            pdfTable.AddCell(SetCell(new CellProperty { Border = Rectangle.TOP_BORDER, Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")), Font = font8, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 15f, Height = 18f }));

            pdfTable.WriteSelectedRows(0, -1, document.LeftMargin, 45, writer.DirectContent);
        }

        private PdfPCell SetCell(CellProperty property)
        {
            PdfPCell tableCell = new PdfPCell(new Phrase(property.Text, property.Font));
            tableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            tableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            tableCell.Border = property.Border ?? Rectangle.NO_BORDER;
            tableCell.Colspan = property.Colspan ?? 1;
            tableCell.Rowspan = property.Rowspan ?? 1;
            tableCell.Rotation = property.Rotation ?? 0;

            if (property.Height != null) tableCell.FixedHeight = property.Height.Value;
            if (property.PaddingLeft != null) tableCell.PaddingLeft = property.PaddingLeft.Value;
            if (property.PaddingBottom != null) tableCell.PaddingBottom = property.PaddingBottom.Value;

            if (property.BackgroundColor != null) tableCell.BackgroundColor = new BaseColor(property.BackgroundColor.Red, property.BackgroundColor.Green, property.BackgroundColor.Blue);

            return tableCell;
        }

        public class CellProperty
        {
            public string Text { get; set; }
            public Font Font { get; set; }
            public int? Rowspan { get; set; }
            public int? Colspan { get; set; }
            public int? HorizontalAlignment { get; set; }
            public int? VerticalAlignment { get; set; }
            public int? Border { get; set; }
            public int? Rotation { get; set; }
            public float? Height { get; set; }
            public float? PaddingLeft { get; set; }
            public float? PaddingBottom { get; set; }
            public Color BackgroundColor { get; set; }
            public class Color
            {
                public int Red { get; set; }
                public int Green { get; set; }
                public int Blue { get; set; }
            }
        }

    }
}