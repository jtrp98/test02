
var initFunc = {
    setDropdown: function (obj, table, firstOption) {

        $.ajax({
            type: "GET",
            url: "/Ashx/InitDropdownData.ashx",
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
    setDropdownDepartmentInSchool: function (obj, table, firstOption, schoolID) {

        $.ajax({
            type: "GET",
            url: "/Ashx/InitDropdownData.ashx",
            data: { table: table, schoolID: schoolID },
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

    }
}