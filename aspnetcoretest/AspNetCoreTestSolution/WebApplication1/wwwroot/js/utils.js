/* json格式转树状结构 
* @param   {json}      json数据 
* @param   {String}    id的字符串 
* @param   {String}    父id的字符串 
* @param   {String}    children的字符串 
* @return  {Array}     数组 
*/
function toTreeData(a, idStr, pidStr, childrenStr) {
    var r = [], hash = {}, len = (a || []).length;
    for (var i = 0; i < len; i++) {
        hash[a[i][idStr]] = a[i];
    }
    for (var j = 0; j < len; j++) {
        var aVal = a[j], hashVP = hash[aVal[pidStr]];
        if (hashVP) {
            !hashVP[childrenStr] && (hashVP[childrenStr] = []);
            hashVP[childrenStr].push(aVal);
        } else {
            r.push(aVal);
        }
    }
    return r;
}

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