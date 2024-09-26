/*The MIT License (MIT)

Copyright (c) 2014 https://github.com/kayalshri/

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

(function ($) {
    $.fn.extend({
        tableExport: function (options) {
            var defaults = {
                separator: ',',
                ignoreColumn: [],
                tableName: 'yourTableName',
                type: 'csv',
                pdfFontSize: 14,
                pdfLeftMargin: 20,
                escape: 'true',
                htmlContent: 'false',
                consoleLog: 'false',
                fileName: 'tableExport',
                sheets: "Repoert"
            };

            var options = $.extend(defaults, options);
            var el = this;

            if (defaults.type == 'csv' || defaults.type == 'txt') {

                // Header
                var tdData = "";
                $(el).find('thead').find('tr').each(function () {
                    tdData += "\n";
                    $(this).filter(':visible').find('th').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                tdData += '"' + parseString($(this)) + '"' + defaults.separator;
                            }
                        }

                    });
                    tdData = $.trim(tdData);
                    tdData = $.trim(tdData).substring(0, tdData.length - 1);
                });

                // Row vs Column
                $(el).find('tbody').find('tr').each(function () {
                    tdData += "\n";
                    $(this).filter(':visible').find('td').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                tdData += '"' + parseString($(this)) + '"' + defaults.separator;
                            }
                        }
                    });
                    //tdData = $.trim(tdData);
                    tdData = $.trim(tdData).substring(0, tdData.length - 1);
                });

                //output
                if (defaults.consoleLog == 'true') {
                    console.log(tdData);
                }
                var base64data = "base64," + $.base64.encode(tdData);
                window.open('data:application/' + defaults.type + ';filename=exportData;' + base64data);
            } else if (defaults.type == 'sql') {

                // Header
                var tdData = "INSERT INTO `" + defaults.tableName + "` (";
                $(el).find('thead').find('tr').each(function () {

                    $(this).filter(':visible').find('th').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                tdData += '`' + parseString($(this)) + '`,';
                            }
                        }

                    });
                    tdData = $.trim(tdData);
                    tdData = $.trim(tdData).substring(0, tdData.length - 1);
                });
                tdData += ") VALUES ";
                // Row vs Column
                $(el).find('tbody').find('tr').each(function () {
                    tdData += "(";
                    $(this).filter(':visible').find('td').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                tdData += '"' + parseString($(this)) + '",';
                            }
                        }
                    });

                    tdData = $.trim(tdData).substring(0, tdData.length - 1);
                    tdData += "),";
                });
                tdData = $.trim(tdData).substring(0, tdData.length - 1);
                tdData += ";";

                //output
                //console.log(tdData);

                if (defaults.consoleLog == 'true') {
                    console.log(tdData);
                }

                var base64data = "base64," + $.base64.encode(tdData);
                window.open('data:application/sql;filename=exportData;' + base64data);


            } else if (defaults.type == 'json') {

                var jsonHeaderArray = [];
                $(el).find('thead').find('tr').each(function () {
                    var tdData = "";
                    var jsonArrayTd = [];

                    $(this).filter(':visible').find('th').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                jsonArrayTd.push(parseString($(this)));
                            }
                        }
                    });
                    jsonHeaderArray.push(jsonArrayTd);

                });

                var jsonArray = [];
                $(el).find('tbody').find('tr').each(function () {
                    var tdData = "";
                    var jsonArrayTd = [];

                    $(this).filter(':visible').find('td').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                jsonArrayTd.push(parseString($(this)));
                            }
                        }
                    });
                    jsonArray.push(jsonArrayTd);

                });

                var jsonExportArray = [];
                jsonExportArray.push({ header: jsonHeaderArray, data: jsonArray });

                //Return as JSON
                //console.log(JSON.stringify(jsonExportArray));

                //Return as Array
                //console.log(jsonExportArray);
                if (defaults.consoleLog == 'true') {
                    console.log(JSON.stringify(jsonExportArray));
                }
                var base64data = "base64," + $.base64.encode(JSON.stringify(jsonExportArray));
                window.open('data:application/json;filename=exportData;' + base64data);
            } else if (defaults.type == 'xml') {

                var xml = '<?xml version="1.0" encoding="utf-8"?>';
                xml += '<tabledata><fields>';

                // Header
                $(el).find('thead').find('tr').each(function () {
                    $(this).filter(':visible').find('th').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                xml += "<field>" + parseString($(this)) + "</field>";
                            }
                        }
                    });
                });
                xml += '</fields><data>';

                // Row Vs Column
                var rowCount = 1;
                $(el).find('tbody').find('tr').each(function () {
                    xml += '<row id="' + rowCount + '">';
                    var colCount = 0;
                    $(this).filter(':visible').find('td').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                xml += "<column-" + colCount + ">" + parseString($(this)) + "</column-" + colCount + ">";
                            }
                        }
                        colCount++;
                    });
                    rowCount++;
                    xml += '</row>';
                });
                xml += '</data></tabledata>'

                if (defaults.consoleLog == 'true') {
                    console.log(xml);
                }

                var base64data = "base64," + $.base64.encode(xml);
                window.open('data:application/xml;filename=exportData;' + base64data);

            } else if (defaults.type == 'excel' || defaults.type == 'doc' || defaults.type == 'powerpoint') {
                // console.log($(this).prop('outerHTML'));
                if ($(this).attr("id") == 'storeProduct') {
                    var excel = "<table style='border-width: 1px;width:40%;'>";
                } else if ($(this).attr("id") == 'dailySales') {
                    var excel = "<table style='border:1'>";
                } else if ($(this).attr("id") == 'salesSeveralDays') {
                    var excel = "<table style='border-width: 1px;width:40%;'>";
                }
                else if ($(this).attr("id") == 'top-upDaily') {
                    var excel = "<table style='border-width: 1px;width:40%;'>";
                }
                else if ($(this).attr("id") == 'top-upSeveral') {
                    var excel = "<table style='border-width: 1px;width:40%;'>";
                }
                else if ($(this).attr("id") == 'reportEmployee') {
                    var excel = "<table style='border-width: 1px;width:40%;'>";
                }
                else if ($(this).attr("id") == 'reportStudent') {
                    var excel = "<table style='border-width: 1px;width:40%;'>";
                } else {//
                    var excel = "<table style='border-width: 1px;'>";
                }

                // Header
                $(el).find('thead').find('tr').each(function () {
                    if ($(this).filter(':visible').find('th').length != 0) {
                        $(this).filter(':visible').find('th').each(function (index, data) {
                            if ($(this).css('display') != 'none') {
                                if ($(this).attr("id") == 'school') {
                                    excel += "<tr>" + $(this).prop('outerHTML');
                                } else if ($(this).attr("id") == 'headdatail') {
                                    excel += "<tr>" + $(this).prop('outerHTML');
                                } else if ($(this).attr("id") == 'dayfall') {
                                    excel += "<tr>" + $(this).prop('outerHTML');
                                } else if ($(this).attr("id") == 'dayshort') {
                                    excel += "<tr>" + $(this).prop('outerHTML');
                                } else if ($(this).attr("id") == 'timetoday') {
                                    excel += "<tr>" + $(this).prop('outerHTML');
                                } else {
                                    excel += "<tr>" + $(this).prop('outerHTML');
                                }
                            }
                        });
                        excel += '</tr>';
                    } else {
                        excel += $(this).prop('outerHTML');
                    }




                });

                // Row Vs Column
                var rowCount = 1;
                $(el).find('tbody').find('tr').each(function () {
                    excel += "<tr>";
                    var colCount = 0;
                    $(this).filter(':visible').find('td').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                if ($(this).attr("id") == 'total') {
                                    excel += "<td style='text-align: right; font-size: 20px;border-style: solid;border-width: 1px;font-weight: bold;' colspan=" + $(this).attr("colspan") + ">" + $(this).html() + "</td>";
                                } else if ($(this).attr("rowspan") !== undefined && $(this).attr("id") == 'headder') {
                                    excel += "<td  style='font-size: 20px;vertical-align:top;text-align: center;border-style: solid;border-width: 1px;font-weight: bold;' rowspan=" + $(this).attr("rowspan") + ">" + $(this).html() + "</td>";
                                } else if ($(this).attr("colspan") !== undefined && $(this).attr("id") == 'headder') {
                                    excel += "<td style='font-size: 20px;border-style: solid;text-align: center;border-width: 1px;font-weight: bold;' colspan=" + $(this).attr("colspan") + ">" + $(this).html() + "</td>";
                                } else if ($(this).attr("rowspan") !== undefined) {
                                    excel += "<td  style='font-size: 18px;vertical-align:top;text-align: center;border-style: solid;border-width: 1px;' rowspan=" + $(this).attr("rowspan") + ">" + $(this).html() + "</td>";
                                } else if ($(this).attr("colspan") !== undefined) {
                                    excel += "<td style='font-size: 18px;border-style: solid;border-width: 1px;' colspan=" + $(this).attr("colspan") + ">" + $(this).html() + "</td>";
                                } else if ($(this).attr("id") == 'headder') {
                                    excel += "<td style=\"text-align: center; font-size: 20px;border-style: solid;border-width: 1px;font-weight: bold;\">" + $(this).html() + "</td>";
                                } else if ($(this).attr("format") == 'string') {
                                    excel += "<td style=\"text-align: center; font-size: 20px;border-style: solid;border-width: 1px;font-weight: bold; mso-number-format: '\@'; \">" + $(this).html() + "</td>";
                                }
                                else {
                                    if ($(this).attr("Int-Length") != undefined) {
                                        var number_format = "0000";
                                        switch ($(this).attr("Int-Length")) {
                                            case "4": number_format = "0000"; break;
                                            case "5": number_format = "00000"; break;
                                            case "6": number_format = "000000"; break;
                                            case "7": number_format = "0000000"; break;
                                            case "8": number_format = "00000000"; break;
                                            case "9": number_format = "000000000"; break;
                                            case "10": number_format = "0000000000"; break;
                                        }
                                        excel += "<td style=\"border-style: solid;border-width: 1px;font-size: 18px;text-align: center; mso-number-format:'" + number_format + "'\">" + $(this).html() + "</td>";
                                    } else {
                                        excel += "<td style=\"border-style: solid;border-width: 1px;font-size: 18px;text-align: center;\">" + $(this).html() + "</td>";
                                    }
                                }
                            }
                        }
                        colCount++;
                    });
                    rowCount++;
                    excel += '</tr>';
                });
                excel += '</table>'

                if (defaults.consoleLog == 'true') {
                    console.log(excel);
                }

                var excelFile = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:" + defaults.type + "' xmlns='http://www.w3.org/TR/REC-html40'>";
                excelFile += "<head>";
                excelFile += "<!--[if gte mso 9]>";
                excelFile += "<xml>";
                excelFile += "<x:ExcelWorkbook>";
                excelFile += "<x:ExcelWorksheets>";
                excelFile += "<x:ExcelWorksheet>";
                excelFile += "<x:Name>";
                excelFile += defaults.sheets;
                excelFile += "</x:Name>";
                excelFile += "<x:WorksheetOptions>";
                excelFile += "<x:DisplayGridlines/>";
                excelFile += "</x:WorksheetOptions>";
                excelFile += "</x:ExcelWorksheet>";
                excelFile += "</x:ExcelWorksheets>";
                excelFile += "</x:ExcelWorkbook>";
                excelFile += "</xml>";
                excelFile += "<![endif]-->";
                excelFile += "</head>";
                excelFile += "<body>";
                excelFile += excel;
                excelFile += "</body>";
                excelFile += "</html>";

                var base64data = "base64," + Base64.encode(excelFile);
                //window.open('data:application/vnd.ms-' + defaults.type + ';filename=exportData.xls;' + base64data);
                downloadFile(defaults.fileName + '.xls',
                    'data:application/xml;charset=utf-8;base64,',
                    excelFile);
            } else if (defaults.type == 'png') {
                html2canvas($(el), {
                    onrendered: function (canvas) {
                        var img = canvas.toDataURL("image/png");
                        window.open(img);


                    }
                });
            } else if (defaults.type == 'pdf') {

                var doc = new jsPDF('p', 'pt', 'a4', true);
                doc.setFontSize(defaults.pdfFontSize);

                // Header
                var startColPosition = defaults.pdfLeftMargin;
                $(el).find('thead').find('tr').each(function () {
                    $(this).filter(':visible').find('th').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                var colPosition = startColPosition + (index * 50);
                                doc.text(colPosition, 20, $(this).html());
                            }
                        }
                    });
                });


                // Row Vs Column
                var startRowPosition = 20; var page = 1; var rowPosition = 0;
                $(el).find('tbody').find('tr').each(function (index, data) {
                    rowCalc = index + 1;

                    if (rowCalc % 26 == 0) {
                        doc.addPage();
                        page++;
                        startRowPosition = startRowPosition + 10;
                    }
                    rowPosition = (startRowPosition + (rowCalc * 10)) - ((page - 1) * 280);

                    $(this).filter(':visible').find('td').each(function (index, data) {
                        if ($(this).css('display') != 'none') {
                            if (defaults.ignoreColumn.indexOf(index) == -1) {
                                var colPosition = startColPosition + (index * 50);
                                doc.text(colPosition, rowPosition, $(this).html());
                            }
                        }

                    });

                });

                // Output as Data URI
                doc.output('datauri');

            }


            function parseString(data) {

                if (defaults.htmlContent == 'true') {
                    content_data = data.html().trim();
                } else {
                    content_data = data.text().trim();
                }

                if (defaults.escape == 'true') {
                    content_data = escape(content_data);
                }



                return content_data;
            }

        }
    });

    function downloadFile(filename, header, data) {
        var DownloadEvt = null;
        var ua = window.navigator.userAgent;
        if (filename !== false && (ua.indexOf("MSIE ") > 0 || !!ua.match(/Trident.*rv\:11\./))) {
            if (window.navigator.msSaveOrOpenBlob)
                window.navigator.msSaveOrOpenBlob(new Blob([data]), filename);
            else {
                // Internet Explorer (<= 9) workaround by Darryl (https://github.com/dawiong/tableExport.jquery.plugin)
                // based on sampopes answer on http://stackoverflow.com/questions/22317951
                // ! Not working for json and pdf format !
                var frame = document.createElement("iframe");

                if (frame) {
                    document.body.appendChild(frame);
                    frame.setAttribute("style", "display:none");
                    frame.contentDocument.open("txt/html", "replace");
                    frame.contentDocument.write(data);
                    frame.contentDocument.close();
                    frame.focus();

                    frame.contentDocument.execCommand("SaveAs", true, filename);
                    document.body.removeChild(frame);
                }
            }
        }
        else {
            var DownloadLink = document.createElement('a');

            if (DownloadLink) {
                var blobUrl = null;

                DownloadLink.style.display = 'none';
                if (filename !== false)
                    DownloadLink.download = filename;
                else
                    DownloadLink.target = '_blank';

                if (typeof data == 'object') {
                    blobUrl = window.URL.createObjectURL(data);
                    DownloadLink.href = blobUrl;
                }
                else if (header.toLowerCase().indexOf("base64,") >= 0)
                    DownloadLink.href = header + Base64.encode(data);
                else
                    DownloadLink.href = header + encodeURIComponent(data);

                document.body.appendChild(DownloadLink);

                if (document.createEvent) {
                    if (DownloadEvt === null)
                        DownloadEvt = document.createEvent('MouseEvents');

                    DownloadEvt.initEvent('click', true, false);
                    DownloadLink.dispatchEvent(DownloadEvt);
                }
                else if (document.createEventObject)
                    DownloadLink.fireEvent('onclick');
                else if (typeof DownloadLink.onclick == 'function')
                    DownloadLink.onclick();

                if (blobUrl)
                    window.URL.revokeObjectURL(blobUrl);

                document.body.removeChild(DownloadLink);
            }
        }
    }

})(jQuery);

