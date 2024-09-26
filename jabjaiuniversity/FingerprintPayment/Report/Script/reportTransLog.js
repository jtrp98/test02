
var $table;
var report_sales = (function () {
    this.reports_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;text-align: center !important;";
    //ar HeaderCSS = "";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding: 0 6px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var color_0 = "background-color:#5db75d;", color_1 = "background-color:#347ab8;", color_2 = "background-color:#f0ad4e;", color_3 = "background-color:#c082ff;";
    var color_4 = "background-color:#9850e0;", color_5 = "background-color:#fb5955;", color_sum = "background-color:#eee;";

    this.RenderHtml = function (table_name, export_file) {
        $("#" + table_name).empty();
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
       
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table

        var report_type = this.reports_data.report_type;
        var sum_money = 0, sum_cost = 0, sum_lucre = 0;
        var sum1 = 0, sum2 = 0, sum3 = 0;
        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "" : "") + "width:50px;"
                    }, {
                        text: "วันที่/เวลา", style: HeaderCSS + (export_file === true ? "" : "") + "width:150px;"
                    }, {
                        text: "ประเภทการใช้", style: HeaderCSS + (export_file === true ? "" : "") + "width:110px;"
                    }, {
                        text: "ร้านค้า", style: HeaderCSS + (export_file === true ? "" : "") + "width:150px;"
                    }, {
                        text: "จำนวนเงิน", style: HeaderCSS + (export_file === true ? "" : "") + "width:100px;"
                    }, {
                        text: "ยอดคงเหลือ", style: HeaderCSS + (export_file === true ? "" : "") + "width:100px;"
                    }, {
                        text: "DeviceId", style: HeaderCSS + (export_file === true ? "" : "") + "width:150px;"
                    }, {
                        text: "QRCode/NFC", style: HeaderCSS + (export_file === true ? "" : "") + "width:130px;"
                    }, {
                        text: "หมายเหตุ", style: HeaderCSS + (export_file === true ? "" : "") + "width:150px;"
                    }]
                }));

        var _usr = this.reports_data.user;
        switch (_usr.type) {

            case 1: $('#lblSummary').text('ชื่อ ' + _usr.info.name + ' (ครู)   ยอด ณ วันที่ (' + dateNow + ') ' + _usr.info.balance);
                break;

            case 2: $('#lblSummary').text('ชื่อ ' + _usr.info.name + ' (นักเรียน)   ยอด ณ วันที่ (' + dateNow + ') ' + _usr.info.balance);
                break;

            default:
                break;
        }

        //console.log(_usr);
        var _last = parseFloat(_usr.last);
        $.each(this.reports_data.data, function (data_Index, data_Values) {
            //sum_money += data_Values.price;
            //sum_cost += data_Values.sum_cost;
            //sum_lucre += data_Values.sum_lucre;

            //sum1 += data_Values.SUM_MONEY;
            //sum2 += data_Values.SUM_WD;
            //sum3 += data_Values.NET;
                              
            var _remark = '', _remarkDate = '';
            var _money = parseFloat(data_Values.nMoney);
            var _symbol = "";
            var _code = "";
            switch (data_Values.Type) {

                case 1:
                    //_remark = ;
                    _remark = (data_Values.tType == 'KIOSK' ? 'เติมเงินผ่านตู้เติมเงิน' : (data_Values.tType == 'WBS1' ? 'เติมเงินโรงเรียน' : 'เติมเงินผ่านแอป'));
                    //_remark = 'เติมเงิน';
                    _last = _last + _money;
                    break;
                case 2: _remark = 'ยกเลิกเติมเงิน';
                    _last = _last - _money;
                    _symbol = "-";
                    break;
                case 3: _remark = 'ถอนเงิน';
                    _last = _last - _money;
                    _symbol = "-";
                    break;
                case 4: _remark = 'ซื้อสินค้า';
                    _last = _last - _money;
                    _symbol = "-";
                    break;
                case 5: _remark = 'ยกเลิกซื้อสินค้า';
                    _last = _last + _money;
                    break;
                case 6: _remark = 'ยืมบัตร';
                    break
                case 7: _remark = 'คืนบัตร';
                    break;
                default:
            }

            if (data_Values.nMoney == 0) {
                _symbol = '';
            }

            if (data_Values.Barcode) {
                _code = 'QRCode : ' + data_Values.Barcode;
            }
            else if (data_Values.NFCOrEncrypt) {
                _code = 'NFC : ' + data_Values.NFCOrEncrypt;
            }

            if (data_Values.Type == 1) {
                if (data_Values.tType == 'MBS1')
                    _remarkDate = (data_Values.Remark + ' ' + (data_Values.txRemark ? data_Values.txRemark + '<br>ยอดตกน้ำวันที่ ' + data_Values.txCreatedTH  : ''));
            }
            else {
                if (data_Values.RemarkDate != null)
                    _remarkDate = 'เวลาซื้อจริง วันที่ ' + data_Values.RemarkDateTH;
            }

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    {
                        text: data_Index + 1, style: RowsCSS
                    },
                    {
                        text: (data_Values.DateTH), style: RowsCSS
                    },
                    {
                        text: _remark, style: RowsCSS //+ 'text-align: left !important;'
                    },
                    {
                        text: ((data_Values.tType == 'MBS1') ? '-' : data_Values.Remark + ' ' + data_Values.Shop), style: RowsCSS
                    },
                    {
                        text: _symbol + "" + (data_Values.nMoney), style: RowsCSS
                    },
                    {
                        text: _last.toFixed(2), style: RowsCSS
                    },
                    {
                        text: data_Values.DeviceID, style: RowsCSS
                    },
                    {
                        text: _code, style: RowsCSS
                    },
                    {
                        text: _remarkDate + (data_Values.TxCount > 1 ? '<i class="material-icons more-one-tx-count" rel="tooltip" data-placement="top" title="Charge: ' + data_Values.ChargeID + '">question_mark</i>' : ''), style: RowsCSS
                    }]
            }));
        });

        $('table.dataTable').hide();
        $('#' + table_name).show();

        $table = $('#' + table_name).DataTable({
            destroy: true,
            paging: false,
            searching: false,
            "info": false

        });
        //t.reload();

        // $("body").mLoading('hide');
    };

    this.RenderHtml2 = function (table_name, export_file) {

        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).empty();
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table

        var report_type = this.reports_data.report_type;
        var sum_money = 0, sum_cost = 0, sum_lucre = 0;
        var sum1 = 0, sum2 = 0, sum3 = 0;
        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:50px;"
                    }, {
                        text: "ประเภทผู้ยืม", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }, {
                        text: "วันที่/เวลา", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }, {
                        text: "ประเภทการใช้", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }, {
                        text: "ร้านค้า", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:180px;"
                    }, {
                        text: "จำนวนเงิน", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:100px;"
                    }, {
                        text: "ยอดคงเหลือ", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:100px;"
                    }, {
                        text: "ค่าประกัน", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:100px;"
                    }, {
                        text: "หมายเหตุ", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }]
                }));

        var _usr = this.reports_data.user;
        $('#lblSummary').text('บัตร ' + _usr.info.name + ' ยอดเงินคงเหลือ ' + _usr.info.balance);
        //switch (_usr.type) {

        //    case 1: $('#lblSummary').text('ชื่อ ' + _usr.info.name + ' (ครู) 	ยอดเงินคงเหลือ ' + _usr.info.balance);
        //        break;

        //    case 2: $('#lblSummary').text('ชื่อ ' + _usr.info.name + ' (นักเรียน)	 ยอดเงินคงเหลือ ' + _usr.info.balance);
        //        break;

        //    default:
        //        break;
        //}

        //console.log(_usr);
        var _last = parseFloat(_usr.last);
        $.each(this.reports_data.data, function (data_Index, data_Values) {
            //sum_money += data_Values.price;
            //sum_cost += data_Values.sum_cost;
            //sum_lucre += data_Values.sum_lucre;

            //sum1 += data_Values.SUM_MONEY;
            //sum2 += data_Values.SUM_WD;
            //sum3 += data_Values.NET;
            var _remark = '', _remarkDate = '';
            var _money = parseFloat(data_Values.nMoney);
            var _insurance = parseFloat(data_Values.Insurance);
            var _symbol = "";
            switch (data_Values.Type) {

                case 1:
                    //_remark = (data_Values.tType == 'WBS1' ? 'เติมเงินโรงเรียน' : 'เติมเงินผ่านแอป');
                    _remark = (data_Values.tType == 'KIOSK' ? 'เติมเงินผ่านตู้เติมเงิน' : (data_Values.tType == 'WBS1' ? 'เติมเงินโรงเรียน' : 'เติมเงินผ่านแอป'));
                    //_remark = 'เติมเงิน';
                    _last = _last + _money;
                    break;
                case 2: _remark = 'ยกเลิกเติมเงิน';
                    _last = _last - _money;
                    _symbol = "-";
                    break;
                case 3: _remark = 'ถอนเงิน';
                    _last = _last - _money + _insurance;
                    _insurance = 0;
                    _symbol = "-";
                    break;
                case 4: _remark = 'ซื้อสินค้า';
                    _last = _last - _money;
                    _symbol = "-";
                    break;
                case 5: _remark = 'ยกเลิกซื้อสินค้า';
                    _last = _last + _money;
                    break;
                case 6: _remark = 'ยืมบัตร';
                    _last = 0;
                    break
                case 7: _remark = 'คืนบัตร';
                    _last = 0;
                    break;
                default:
            }

            if (data_Values.nMoney == 0) {
                _symbol = '';
            }

            if (data_Values.Type == 1) {
                if (data_Values.tType == 'MBS1')
                    _remarkDate = data_Values.Remark;
            }
            else {
                if (data_Values.RemarkDate != null)
                    _remarkDate = 'เวลาซื้อจริง วันที่ ' + moment(data_Values.RemarkDate).format("DD/MM/YYYY, HH:mm:ss");
            }

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    {
                        text: data_Index + 1, style: RowsCSS
                    },
                    {
                        text: data_Values.User, style: RowsCSS
                    },
                    {
                        text: moment(data_Values.Date)//.add(543, 'year')
                            .format("DD/MM/YYYY, HH:mm:ss"), style: RowsCSS
                    },
                    {
                        text: _remark, style: RowsCSS //+ 'text-align: left !important;'
                    },
                    {
                        text: ((data_Values.tType == 'MBS1') ? '-' : data_Values.Remark + ' ' + data_Values.Shop), style: RowsCSS
                    },
                    {
                        text: (data_Values.Type == 6 || data_Values.Type == 7) ? '' : _symbol + "" + (data_Values.nMoney), style: RowsCSS
                    },
                    {
                        text: (data_Values.Type == 6 || data_Values.Type == 7) ? '' : _last.toFixed(2), style: RowsCSS
                    },
                    {
                        text: (data_Values.Type == 6 || data_Values.Type == 7) ? '' : _insurance, style: RowsCSS
                    },
                    {
                        text: _remarkDate, style: RowsCSS
                    }]
            }));
        });
                
        $('table.dataTable').hide();
        $('#' + table_name).show();
        $table = $('#' + table_name).DataTable({
            destroy: true,
            paging: false,
            searching: false,
            "info": false
        });

        //$("body").mLoading('hide');
    };

    this.export_excel = function () {
        var t = $('#type').val();
        if (t == "1") {
            //$("body").mLoading();
            var dt = new Date();
            var json;
            var xhr;
            var report_type = this.reports_data.report_type;
            var file_name = 'รายงานข้อมูลการใช้บัตร(รายละเอียด)_' + dt.format("ddMMyyyyHHmmssss") + '.xls';
            //this.RenderHtml_Detail('table_exports', true);
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/Report/reporttranslog.aspx/exportExcel", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                //$("body").mLoading('hide');
            };
            xhr.send(json);
        }
        else {
            //$("body").mLoading();
            var dt = new Date();
            var json;
            var xhr;
            var report_type = this.reports_data.report_type;
            var file_name = 'รายงานข้อมูลการใช้บัตรสำรอง(รายละเอียด)_' + dt.format("ddMMyyyyHHmmssss") + '.xls';
            //this.RenderHtml_Detail('table_exports', true);
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/Report/reporttranslog.aspx/exportTempExcel", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                // $("body").mLoading('hide');
            };
            xhr.send(json);
        }

    };



});
