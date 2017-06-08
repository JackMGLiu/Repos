$.request = function (keyValue) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == keyValue) {
            if (unescape(ar[1]) === 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
};

$.isNullOrEmpty = function (obj) {
    if ((typeof (obj) === "string" && obj === "") || obj == null || obj === undefined) {
        return true;
    } else {
        return false;
    }
};

$.fn.SetWebControls = function (data) {
    var $id = $(this);
    for (var key in data) {
        var id = $id.find('#' + key);
        if (id.attr('id')) {
            var type = id.attr('type');
            if (id.hasClass("input-datepicker")) {
                type = "datepicker";
            }
            if (id.hasClass("Wdate")) {
                type = "datepicker";
            }
            if (id.attr('lay-skin') == 'switch') {
                type = "checkswitch";
            }

            var value = $.trim(data[key]).replace(/&nbsp;/g, '');
            switch (type) {
                case "checkbox":
                    if (value === 1 || value === '1') {
                        id.attr("checked", 'checked');
                    } else {
                        id.removeAttr("checked");
                    }
                    break;
                case "checkswitch":
                    if (value === 1 || value === '1') {
                        id.attr("checked", 'checked');
                    } else {
                        id.removeAttr("checked");
                    }
                    break;
                case "select":
                    id.val(value);
                    //id.ComboBoxSetValue(value);
                    break;
                case "selectTree":
                    //id.ComboBoxTreeSetValue(value);
                    break;
                case "datepicker":
                    //id.val(formatDate(value, 'yyyy-MM-dd'));
                    //id.val(DateFormat(value));
                    break;
                default:
                    id.val(value);
                    break;
            }
        }
    }
};