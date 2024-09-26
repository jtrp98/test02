
var comFunction = {
    getFilenamePathFromUrl: function () {

        var url = window.location.pathname;
        var filename = url.substring(url.lastIndexOf('/') + 1);

        return filename;
    }
}
