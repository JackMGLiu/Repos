/*设置cookie*/
function setCookie(name, value, days) {
    if (days == null || days == '') {
        days = 300;
    }
    var exp = new Date();
    exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + "; path=/;expires=" + exp.toGMTString();
}

/*获取cookie*/
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

/*ajax请求*/
function ajax(url, param, datat, callback) {
    $.ajax({
        type: "post",
        url: url,
        data: param,
        dataType: datat,
        success: function (data) {
            callback;
        },
        error: function () {
            alert("失败..");
        }
    });
}

var utils = {};

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


