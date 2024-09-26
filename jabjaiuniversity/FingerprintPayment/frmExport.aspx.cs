using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/*** Using For Convert PDF ***/
using iText = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class frmExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnExport.Click += BtnExport_Click;
            btnExportPdf.Click += BtnExportPdf_Click;
            ListData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/x-msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename = report" + DateTime.Today.ToString("yyyyMMddHHmmss") + ".xls");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            tbl.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();
        }
        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            string _fliename = DateTime.Now.ToString("yyyyMMddHHmmss");
            string ServerPathDelete = "";
            ServerPathDelete = Server.MapPath("./") + "UploadFile\\temp\\pdf\\" + _fliename + ".pdf";
            string PDFFileNameOpen = "./UploadFile/temp/pdf/" + _fliename + ".pdf";
            createPDF(_fliename);

            if (!Directory.Exists(ServerPathDelete))
            {
                createPDF(GetPageCount(ServerPathDelete), ServerPathDelete, _fliename);
                //if (ConfigurationSettings.AppSettings["Encrypt"].ToString().Equals("1")) //ถ้าโหมดเป็นโหมด Encrypt=1
                //{
                //    Encode = Server.UrlEncode(STCrypt.Encrypt(PDFFileNameOpen + "&" + ServerPathDelete));
                //}
                //else
                //{
                //    Encode = Server.UrlEncode(PDFFileNameOpen + "&" + ServerPathDelete);
                //}
                //ScriptManager.RegisterStartupScript(this, GetType(), "printReport", "window.open('openPDF.aspx?str=" + Encode + "');", true);
            }
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=report_" + DateTime.Today.ToString("ddMMyyyyHHmmss") + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //tbl.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
        }

        private void ListData()
        {
            string dstart = Request.QueryString["dstart"];
            string dend = Request.QueryString["dend"];
            string status = Request.QueryString["status"];
            string sublv = Request.QueryString["sublv"];
            string sublv2 = Request.QueryString["sublv2"];
            string tremid = Request.QueryString["tremid"];
            string _comm = "", _comm2 = "", SQL = "", _SubLevel = "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            string id = "";

            if (string.IsNullOrEmpty(tremid))
            {
                #region Employees
                if (!string.IsNullOrEmpty(id))
                {
                    _comm += " AND sEmp = " + id;
                }
                if (!string.IsNullOrEmpty(dstart))
                {
                    try
                    {
                        _dstart = DateTime.ParseExact(dstart, "MM/dd/yyyy", new CultureInfo("en-us"));
                    }
                    catch { }
                }
                if (!string.IsNullOrEmpty(dend))
                {
                    try
                    {
                        _dend = DateTime.ParseExact(dend, "MM/dd/yyyy", new CultureInfo("en-us"));
                    }
                    catch { }
                }

                if (status != "")
                {
                    if (status != "3")
                    {
                        status = "AND SIN = '" + status + "'";
                    }
                    else
                    {
                        status = "AND SIN = '3' OR SIN IS NULL";
                    }
                }

                SQL = @"SELECT * FROM(SELECT TB2.dScan,TB2.sEmp,sName,sLastname
            ,MAX(_TimeIn) AS dTimeIn,MAX(_TimeOut) AS 'dTimeOut'
            ,ISNULL(MAX(StatusIN),'3') AS SIN,ISNULL(MAX(StatusOUT),'3') AS SOUT
            ,MyRowNumber
            FROM  (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            FROM sEmpList ('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"','" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t) AS TB2
            LEFT JOIN
            (
            (SELECT LogEmpDate,sEmp,LogEmpTime AS _TimeIn,LogEmpScanStatus AS StatusIN,'' AS '_TimeOut','' AS 'StatusOUT'
            FROM TLogEmpTimeScan   AS TBIN
            WHERE LogEmpType = '0')
            UNION ALL
            (SELECT LogEmpDate,sEmp,'','',LogEmpTime AS _TimeOut,LogEmpScanStatus AS StatusOUT
            FROM TLogEmpTimeScan  AS TBOUT
            WHERE LogEmpType = '1') ) AS TB1
            ON TB1.sEmp = TB2.sEmp AND TB2.dScan = LogEmpDate
            GROUP BY TB2.dScan,LogEmpDate,TB2.sEmp,sName,sLastname,MyRowNumber
           ) TB01
		   WHERE 1=1 " + _comm + @"
		    ORDER BY dScan DESC,sEmp,sName";

                #endregion
            }
            else
            {
                //_comm += " and MyRowNumber between " + ((_index * 20) - 19) + " and " + (_index * 20) + "";
                if (!string.IsNullOrEmpty(sublv)) _comm2 += " AND t.nTSubLevel  = " + sublv;
                if (!string.IsNullOrEmpty(sublv2)) _comm2 += " AND t.nTermSubLevel2  = " + sublv2;

                if (!string.IsNullOrEmpty(id))
                {
                    _comm2 += " AND t.sEmp = " + id;
                }

                if (string.IsNullOrEmpty(dstart))
                {
                    foreach (DataRow _dr in fcommon.Get_Data(fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + ""), " select * from TTerm where nTerm = '" + string.Format("TS{0:0000000}", int.Parse(tremid)) + "'").Rows)
                    {
                        _dstart = DateTime.Parse(_dr["dstart"] + "");
                        _dend = DateTime.Parse(_dr["dend"] + "");
                        if (DateTime.Today < _dend)
                        {
                            _dend = DateTime.Today;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dstart))
                    {
                        try
                        {
                            _dstart = DateTime.ParseExact(dstart, "dd/MM/yyyy", new CultureInfo("en-us"));
                        }
                        catch { }
                    }
                    if (!string.IsNullOrEmpty(dend))
                    {
                        try
                        {
                            _dend = DateTime.ParseExact(dend, "dd/MM/yyyy", new CultureInfo("en-us"));
                        }
                        catch { }
                    }
                }

                if (status != "")
                {
                    if (status != "3")
                    {
                        status = "AND StatusIN = '" + status + "'";
                    }
                    else
                    {
                        status = "AND StatusIN = '3' OR StatusIN IS NULL";
                    }

                }

                SQL = @"SELECT sEmp,sName,sLastname,dScan,_TimeIn AS 'dTimeIn',_TimeOut AS 'dTimeOut'
             ,ISNULL(StatusIN,'3') AS SIN,StatusOUT AS SOUT,TB2.SubLevel , TB2.nTSubLevel2
            FROM
            (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            ,t.nMoney,t.nMax,CONVERT(varchar,t.nTSubLevel2) AS nTSubLevel2,t.SubLevel
            FROM StudentList('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"'
            ,'" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t where 1=1 " + _comm2 + @") AS TB2
            LEFT JOIN
            (
            SELECT LogDate,sID,MAX(_TimeIn) AS _TimeIn,MAX(_TimeOut) AS _TimeOut
            ,ISNULL(MAX(StatusIN),'3') AS StatusIN,ISNULL(MAX(StatusOUT),'3') AS StatusOUT
            FROM
            (SELECT  LogDate,sID,LogTime AS _TimeIn,LogScanStatus AS StatusIN,NULL AS '_TimeOut',NULL AS 'StatusOUT'
            FROM TLogUserTimeScan 
            WHERE LogType = '0'
            UNION
            SELECT LogDate,sID,NULL,NULL,LogTime AS _TimeOut,LogScanStatus AS StatusOUT
            FROM TLogUserTimeScan  AS TBOUT
            WHERE LogType = '1') AS TB1
            GROUP BY LogDate,sID) AS TB3 ON TB2.sEmp = TB3.sID AND TB2.dScan = LogDate
            WHERE 1 = 1 " + status + _comm + @"
            ORDER BY dScan DESC,TB2.SubLevel,TB2.nTSubLevel2,sName";

            }
            DataTable dtUser = fcommon.Get_Data(fcommon.ConfigSqlConnection(Session["sEntities"] + ""), SQL);
            string _dScan = "";
            string[] _colName = { "sName", "sLastname", "dTimeIn", "SIN", "dTimeOut", "SOUT", "" };
            string[] _colHeaderName = { "ชื่อ", "นามสกุล", "เวลาเข้า", "สถานะ", "เวลาออก", "สถานะ", "หมายเหตุ" };
            foreach (DataRow _dr in dtUser.Rows)
            {
                HtmlTableRow _HtmlRow = new HtmlTableRow();

                if (_dScan == "" || _dScan != DateTime.Parse(_dr["dScan"] + "").ToString("dd/MM/yyyy"))
                {
                    HtmlTableRow _HtmlRowHeaderDay = new HtmlTableRow();
                    HtmlTableCell _HtmlCell = new HtmlTableCell();
                    _HtmlCell.InnerHtml = DateTime.Parse(_dr["dScan"] + "").ToString("dd/MM/yyyy");
                    _HtmlCell.ColSpan = 7;
                    _HtmlCell.Style.Add(" border-bottom", " 1px solid");
                    _HtmlRowHeaderDay.Cells.Add(_HtmlCell);
                    tbl.Rows.Add(_HtmlRowHeaderDay);
                    _dScan = DateTime.Parse(_dr["dScan"] + "").ToString("dd/MM/yyyy");

                    HtmlTableRow _HtmlRowHeaderName = new HtmlTableRow();
                    foreach (string _col in _colHeaderName)
                    {
                        HtmlTableCell _HtmlCellHeadr = new HtmlTableCell();
                        _HtmlCellHeadr.InnerHtml = _col;
                        _HtmlCellHeadr.Style.Add("text-align", "center");
                        _HtmlCellHeadr.Style.Add("border", " 1px solid");
                        _HtmlCellHeadr.Style.Add("color", "#FFF");
                        _HtmlCellHeadr.Style.Add("background-color", "#337ab7");
                        _HtmlCellHeadr.SkinID = "warning";
                        _HtmlRowHeaderName.Cells.Add(_HtmlCellHeadr);
                    }
                    tbl.Rows.Add(_HtmlRowHeaderName);
                }

                if (!string.IsNullOrEmpty(tremid) && (_SubLevel != _dr["SubLevel"].ToString().Trim() + " / " + _dr["nTSubLevel2"].ToString()))
                {
                    HtmlTableRow _HtmlRowHeaderDay = new HtmlTableRow();
                    HtmlTableCell _HtmlCell = new HtmlTableCell();
                    _HtmlCell.InnerHtml = _dr["SubLevel"].ToString().Trim() + " / " + _dr["nTSubLevel2"].ToString();
                    _HtmlCell.ColSpan = 7;
                    _HtmlCell.Style.Add(" border", " 1px solid");
                    _HtmlCell.Style.Add(" color", " 1px solid");
                    _HtmlCell.Style.Add("background-color", "#B4C6E7");
                    _HtmlRowHeaderDay.Cells.Add(_HtmlCell);
                    tbl.Rows.Add(_HtmlRowHeaderDay);
                    _SubLevel = _dr["SubLevel"].ToString().Trim() + " / " + _dr["nTSubLevel2"].ToString();

                }

                foreach (string _col in _colName)
                {

                    HtmlTableCell _HtmlCell = new HtmlTableCell();
                    if (_col == "")
                    {
                        _HtmlCell.InnerHtml = "-";
                    }
                    else
                    {

                        switch (_col)
                        {
                            case "dTimeIn":
                            case "dTimeOut":
                                if (!string.IsNullOrEmpty(_dr[_col] + "") && _dr[_col] + "" != "00:00:00")
                                    _HtmlCell.InnerHtml = (_dr[_col] + "").Split('.')[0];
                                else
                                    _HtmlCell.InnerHtml = "-";
                                _HtmlCell.Style.Add("text-align", "center");
                                break;
                            case "SIN":
                                switch ((_dr[_col] + "").Trim())
                                {
                                    case "0":
                                        _HtmlCell.InnerHtml = "ตรงเวลา";
                                        break;
                                    case "1":
                                        _HtmlCell.InnerHtml = "สาย";
                                        break;
                                    case "3":
                                        _HtmlCell.InnerHtml = "ขาด";
                                        break;
                                    case "9":
                                        _HtmlCell.InnerHtml = "หยุด";
                                        break;
                                }
                                _HtmlCell.Style.Add("text-align", "center");
                                break;
                            case "SOUT":
                                switch ((_dr[_col] + "").Trim())
                                {
                                    case "0":
                                        _HtmlCell.InnerHtml = "ตรงเวลา";
                                        break;
                                    case "1":
                                        _HtmlCell.InnerHtml = "ออกช้า";
                                        break;
                                    case "2":
                                        _HtmlCell.InnerHtml = "ออกก่อน";
                                        break;
                                    default:
                                        _HtmlCell.InnerHtml = (_dr["SIN"] + "").Trim() == "9" ? "หยุด" : "ขาด";
                                        break;
                                }
                                _HtmlCell.Style.Add("text-align", "center");
                                break;
                            default:
                                _HtmlCell.InnerHtml = _dr[_col] + "";
                                break;

                        }

                    }
                    _HtmlCell.Style.Add("border", " 1px solid");
                    _HtmlRow.Cells.Add(_HtmlCell);
                }
                tbl.Rows.Add(_HtmlRow);
            }
        }

        #region PDF
        #region //===============-> create PDF <-===============//
        private void createPDF(int _pageCount, string _existingPDF, string _sfilename)//แปะ จำนวนหน้า
        {
            #region font
            BaseFont EnCodefont = BaseFont.CreateFont(Server.MapPath("./fonts/ANGSA.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font Bfont = new iTextSharp.text.Font(EnCodefont, 16, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Nfont = new iTextSharp.text.Font(EnCodefont, 16, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Ufont = new iTextSharp.text.Font(EnCodefont, 16, iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            #endregion
            #region// Create Directory (เช็ค และสร้างไดเร็คเตอรี)

            if (!Directory.Exists(Server.MapPath("./") + "UploadFile\\temp\\pdf\\"))
            {
                Directory.CreateDirectory(Server.MapPath("./") + "UploadFile\\temp\\pdf\\");
            }
            #endregion
            string fileName = "./UploadFile/temp/pdf/" + _sfilename + ".pdf";
            iText.Document document = new iText.Document(iText.PageSize.A4.Rotate(), 25, 25, 5, 5);
            FileStream fs = new FileStream(Server.MapPath(fileName), FileMode.Create);
            PdfWriter.GetInstance(document, fs);
            if (File.Exists(fileName))
                File.Delete(fileName);

            // insert footer 
            iText.HeaderFooter footer = new iText.b(new iText.Phrase("หน้าที่ ", Nfont), new iText.Phrase(" / " + _pageCount.ToString().Trim() + "", Nfont));
            footer.Border = iText.Rectangle.NO_BORDER;
            footer.Alignment = iText.Rectangle.ALIGN_CENTER;
            document.Footer = footer;
            document.Header = CreateHeader();
            document.Open();
            loadDocument(document);
            if (document != null)
            {
                document.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "printReport", "window.open('" + fileName + "');", true);
            }
        }
        private void createPDF(string _filename)
        {
            #region font
            BaseFont EnCodefont = BaseFont.CreateFont(Server.MapPath("./fonts/ANGSA.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font Bfont = new iTextSharp.text.Font(EnCodefont, 16, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Nfont = new iTextSharp.text.Font(EnCodefont, 16, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Ufont = new iTextSharp.text.Font(EnCodefont, 16, iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            #endregion
            // step 1: creation of a document-object
            iText.Document document = new iText.Document(iText.PageSize.A4.Rotate(), 25, 25, 5, 25);

            //try
            //{
            #region// Create Directory (เช็ค และสร้างไดเร็คเตอรี)
            if (!Directory.Exists(Server.MapPath("./") + "UploadFile\\temp\\pdf\\"))
                Directory.CreateDirectory(Server.MapPath("./") + "UploadFile\\temp\\pdf\\");
            #endregion

            // step 2: we create a writer that listens to the document
            string fileName = "./UploadFile/temp/pdf/" + _filename + ".pdf";
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath("./UploadFile/temp/pdf/") + _filename + ".pdf", FileMode.Create));
            if (File.Exists(fileName))
                File.Delete(fileName);

            //กรณี ต้องการ ให้ใส่ footer ทุกหน้า ให้ แปะโค๊ดส่วนนี้ ไปก่อนที่จะเขียน คำสั่ง document.Open(); เพราะ ถ้าใส่โค๊ด footer หลัง เปิด document หน้าแรก จะไม่มี footer
            document.Header = CreateHeader();
            iText.HeaderFooter footer = new iText.HeaderFooter(new iText.Phrase("หน้าที่ ", Nfont), true);
            footer.Border = iText.Rectangle.NO_BORDER;
            footer.Alignment = iText.Rectangle.ALIGN_CENTER;
            document.Footer = footer;

            // step 3: we open the document
            document.Open();

            // step 4: we add content to the document 
            loadDocument(document);
            //}
            //catch (Exception e2)
            //{
            //    Console.WriteLine(e2);
            //}
            document.Close();

        }
        private int GetPageCount(string _path)
        {
            PdfReader pdf_fie = new PdfReader(_path);
            int page_count = pdf_fie.NumberOfPages;
            pdf_fie.Close();
            return page_count;
        }
        private iText.HeaderFooter CreateHeader()
        {
            iText.Phrase p = new iText.Phrase();
            #region กำหนด ฟอนต์
            BaseFont EnCodefont = BaseFont.CreateFont(Server.MapPath("./fonts/ANGSA.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font Bfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Nfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Ufont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Mfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font MBfont = new iTextSharp.text.Font(EnCodefont, 12, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            #endregion

            #region สร้างหัวรายงาน
            int[] swidth = { 20, 10, 30, 10, 15, 15 };// percentage
            iText.Table datatable = new iText.Table(6);
            datatable.Padding = 1;
            datatable.Cellpadding = 3;
            datatable.Cellspacing = 2;
            datatable.SetWidths(swidth);
            datatable.WidthPercentage = 100; // percentage
            datatable.Border = 0;

            #endregion
            p.Add(datatable);
            iText.HeaderFooter header = new iText.HeaderFooter(p, false);
            header.Border = iText.Rectangle.NO_BORDER;
            header.Alignment = iText.Rectangle.ALIGN_CENTER;
            return header;
        }
        private void loadDocument(iTextSharp.text.Document document)
        {
            #region กำหนด ฟอนต์
            BaseFont EnCodefont = BaseFont.CreateFont(Server.MapPath("./fonts/ANGSA.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font Bfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Nfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Ufont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Mfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font MBfont = new iTextSharp.text.Font(EnCodefont, 12, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(System.Drawing.Color.Black));
            iTextSharp.text.Font Wfont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.White));
            iTextSharp.text.Font Bluefont = new iTextSharp.text.Font(EnCodefont, 14, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(System.Drawing.Color.Blue));
            #endregion
            #region PartBody
            iText.Table dtBody;
            int[] swidth_Body = { 35, 10, 10, 10, 10, 25 };// percentage
            dtBody = new iText.Table(6);
            dtBody.Padding = 1;
            dtBody.Cellpadding = 2;
            dtBody.Cellspacing = 1;
            dtBody.SetWidths(swidth_Body);
            dtBody.WidthPercentage = 100; // percentage
            dtBody.Border = 0;

            #region HeadTableBody
            iTextSharp.text.Cell CellBody = new iTextSharp.text.Cell(new iTextSharp.text.Phrase("", Nfont));
            CellBody.Colspan = 6;
            CellBody.HorizontalAlignment = iText.Rectangle.ALIGN_CENTER;
            CellBody.VerticalAlignment = iText.Rectangle.ALIGN_MIDDLE;
            CellBody.BorderColor = new iText.Color(System.Drawing.Color.White);
            dtBody.AddCell(CellBody);

            #region TableBody
            foreach (HtmlTableRow tbr in tbl.Rows)
            {
                if (tbr.Cells.Count == 1)
                {
                    CellBody = new iTextSharp.text.Cell(new iTextSharp.text.Phrase(tbr.Cells[0].InnerHtml, Nfont));
                    CellBody.Colspan = 6;
                    CellBody.HorizontalAlignment = iText.Rectangle.ALIGN_LEFT;
                    CellBody.VerticalAlignment = iText.Rectangle.ALIGN_MIDDLE;
                    CellBody.BorderColor = new iText.Color(System.Drawing.Color.Black);
                    dtBody.AddCell(CellBody);
                }
                else
                {
                    for (int i = 0; i < tbr.Cells.Count; i++)
                    {
                        CellBody = new iTextSharp.text.Cell(new iTextSharp.text.Phrase(tbr.Cells[i].InnerHtml, Nfont));
                        CellBody.Colspan = 1;
                        CellBody.HorizontalAlignment = iText.Rectangle.ALIGN_CENTER;
                        CellBody.VerticalAlignment = iText.Rectangle.ALIGN_MIDDLE;
                        CellBody.BorderColor = new iText.Color(System.Drawing.Color.Black);
                        dtBody.AddCell(CellBody);
                    }
                }
            }
            #endregion
            #endregion
            document.Add(dtBody);

            #endregion
        }
        #endregion
        #endregion
    }
}