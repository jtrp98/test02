
var initFunction = {
    setDropdown: function (obj, table, firstOption) {

        $.ajax({
            type: "GET",
            url: "/AssetManagement/Ashx/InitDropdownData.ashx",
            data: { table: table },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {

                $(obj).empty();

                var options = firstOption;
                for (var i = 0; i < result.length; i++) {

                    options += '<option value="' + result[i].id + '">' + result[i].value + '</option>';

                }

                $(obj).html(options);
            }
        });
    },

    setDropdownWithCatePara: function (obj, table, cateID, firstOption) {

        $.ajax({
            type: "GET",
            url: "/AssetManagement/Ashx/InitDropdownData.ashx",
            data: { table: table, cateID: cateID },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {

                $(obj).empty();

                var options = firstOption;
                for (var i = 0; i < result.length; i++) {

                    options += '<option value="' + result[i].id + '">' + result[i].value + '</option>';

                }

                $(obj).html(options);
            }
        });
    },

    setDropdownWithCode: function (obj, table, firstOption) {

        $.ajax({
            type: "GET",
            url: "/AssetManagement/Ashx/InitDropdownData.ashx",
            data: { table: table },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {

                $(obj).empty();

                var options = firstOption;
                for (var i = 0; i < result.length; i++) {

                    options += '<option value="' + result[i].id + '" code="' + result[i].code + '">' + result[i].value + '</option>';

                }

                $(obj).html(options);
            }
        });
    }
}