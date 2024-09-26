class TemplateTable extends Array {

    PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber }) {
        this.page = { 'pageSize': pageSize, 'pageNumber': pageNumber };
    }

    TemplateSetting({ 'template_id': template_id, 'target_name': target_name }) {
        this.template = { 'template_id': template_id, 'target_name': target_name };
    }

    RenderRows(json_data) {
        let page = this.page;
        let template = this.template;
        const foot = json_data.FOOT;
        const data = json_data.DATA;
        var template_html = $(template.template_id).html();
        Mustache.parse(template_html);   // optional, speeds up future uses
        var rendered = Mustache.render(template_html, json_data);
        $(template.target_name).html(rendered);

        $("#pageNumber option").remove();
        var i = 1;
        if (foot.pageSize > 0) {
            while (i <= foot.pageSize) {
                $("#pageNumber").append($("<option></option>")
                    .attr("value", i).text(i));
                i++;
            }
        }
        else {
            $("#pageNumber").append($("<option></option>")
                .attr("value", i).text(i));
        }

        $("#pageNumber").val(page.pageNumber);
        $("#spane_pageNumber").html("หน้าที่ " + page.pageNumber + " จากทั้งหมด " + foot.pageSize + " หน้า");
        if ($("#pageNumber").val() === "1") $("#backbutton").hide();
        else $("#backbutton").show();
        if (parseInt($("#pageNumber").val()) === $("#pageNumber option").length) $("#nextbutton").hide();
        else $("#nextbutton").show();
        $('.selectpicker').selectpicker('refresh');
    }
}