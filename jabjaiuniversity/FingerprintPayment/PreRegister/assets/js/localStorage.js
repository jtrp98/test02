// Manage localStorage
var ls = {
    isBrowserSupport: function () {

        return (typeof (Storage) !== "undefined");

    },
    setItem: function (key, jsonData) {

        window.localStorage.setItem(key, JSON.stringify(jsonData));

    },
    getItem: function (key) {

        if (window.localStorage.getItem(key) === null) {

            ls.setItem(key, {});

        }

        return JSON.parse(window.localStorage.getItem(key));

    },
    removeItem: function (key) {

        window.localStorage.removeItem(key);

    }
}