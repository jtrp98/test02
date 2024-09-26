$(function () {
    $(".jabjai-toolbar-link").click(function () {
        $(".jabjai-right-panel").addClass("jabjai-panel--on");
        return false;
    });

    $(".jabjai-panel__close").click(function () {
        $(".jabjai-right-panel").removeClass("jabjai-panel--on");
        return false;
    });


    $("#ddl-option").change(function () {
        if ($(this).val() === "0") {
            $("#header-page").removeClass("hide");
            $("#page2").css("padding-top","");
        } else {
            $("#page2").css("padding-top","100px");
            $("#header-page").addClass("hide");
        }
    });
});


function print() {
    $("#pageTemp").html('');
    $("#page1").clone().appendTo("#pageTemp");
    $("#pageTemp").find(".hide").remove();
    var contents = $("#pageTemp").html();
    var frame1 = document.createElement('iframe');
    frame1.name = "frame1";
    frame1.style.position = "absolute";
    frame1.style.top = "-1000000px";
    document.body.appendChild(frame1);
    var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
    frameDoc.document.open();
    frameDoc.document.write("<html><meta http-equiv='cache-control' content='no-cache'><head><title></title>");
    frameDoc.document.write("<link rel=\"stylesheet\" href=\"/bootstrap%20SB2/bower_components/bootstrap/dist/css/bootstrap.css\" type=\"text/css\"/>");
    frameDoc.document.write("<link rel=\"stylesheet\" href=\"/styles/style.css\" type=\"text/css\"/>");
    frameDoc.document.write("<link rel=\"stylesheet\" href=\"/Content/print.css\" type=\"text/css\"/>");
    frameDoc.document.write("<script type=\"text/javascript\" src=\"/Scripts/jquery-1.10-2.min.js\" /></script>");
    frameDoc.document.write('</head><body>');
    frameDoc.document.write(contents);
    frameDoc.document.write('</body></html>');
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
        document.body.removeChild(frame1);
    }, 500);
    return false;

}