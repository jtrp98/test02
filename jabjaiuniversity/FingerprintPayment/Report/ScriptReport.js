function RenderRows() {
    var RowsHtml = "";
    $.each(arguments, function (index, array_data) {
        RowsHtml += "<tr class=\"" + array_data.class + "\" style=\"" + array_data.style + "\" >";
        $.each(array_data.data, function (index, data) {
            if (!isEmptyObject(data)) {

                if (data.fun !== undefined) data.fun();

                RowsHtml += "<" + (array_data.rowtype === "header" ? "th" : "td") +
                    " colspan='" + (data.colspan === undefined ? 1 : data.colspan) +
                    "' rowspan='" + (data.rowspan === undefined ? 1 : data.rowspan) +
                    "' class='" + data.class + "' " +
                    "' style='" + data.style + "' " +
                    (data.row === undefined ? "" : " row='" + data.row + "'") +
                    (data.column === undefined ? "" : " column='" + data.column + "'") +
                    (data.attribute === undefined ? "" : data.attribute) +
                    "> " + data.text;
            }                       
        });
    });
    return RowsHtml;
}

function isEmptyObject(obj) {
    for (const prop in obj) {
        if (Object.hasOwn(obj, prop)) {
            return false;
        }
    }

    return true;
}

function OpenWindowWithPost(url, windowoption, name, params) {
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", url);
    form.setAttribute("target", name);

    for (var i in params) {
        if (params.hasOwnProperty(i)) {
            var input = document.createElement('input');
            input.type = 'hidden';
            input.name = i;
            input.value = params[i];
            form.appendChild(input);
        }
    }

    document.body.appendChild(form);

    //note I am using a post.htm page since I did not want to make double request to the page 
    //it might have some Page_Load call which might screw things up.
    var w1 = window.open("post.htm", name, windowoption);

    form.submit();

    document.body.removeChild(form);

}

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
                frame.contentDocument.open("application/x-msexcel", "replace");
                frame.contentDocument.write(data);
                frame.contentDocument.close();
                frame.focus();

                frame.contentDocument.execCommand("SaveAs", true, filename);
                document.body.removeChild(frame);
            }
        }
    } else {
        var DownloadLink = document.createElement('a');

        if (DownloadLink) {
            var blobUrl = null;

            DownloadLink.style.display = 'none';
            if (filename !== false)
                DownloadLink.download = filename;
            else
                DownloadLink.target = '_blank';

            if (typeof data === 'object') {
                var binaryData = [];
                binaryData.push(data);
                blobUrl = window.URL.createObjectURL(new Blob(binaryData, { type: "data:application/vnd.ms-excel" }));

                //blobUrl = window.URL.createObjectURL(data);
                DownloadLink.href = blobUrl;
            } else if (header.toLowerCase().indexOf("base64,") >= 0)
                DownloadLink.href = header + Base64.encode(data);
            else
                DownloadLink.href = header + encodeURIComponent(data);

            document.body.appendChild(DownloadLink);

            if (document.createEvent) {
                if (DownloadEvt === null)
                    DownloadEvt = document.createEvent('MouseEvents');

                DownloadEvt.initEvent('click', true, false);
                DownloadLink.dispatchEvent(DownloadEvt);
            } else if (document.createEventObject)
                DownloadLink.fireEvent('onclick');
            else if (typeof DownloadLink.onclick === 'function')
                DownloadLink.onclick();

            if (blobUrl)
                window.URL.revokeObjectURL(blobUrl);

            document.body.removeChild(DownloadLink);
        }
    }
}