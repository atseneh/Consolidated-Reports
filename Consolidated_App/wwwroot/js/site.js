﻿
function addCommas(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function clearValue(id) {
    var formatedId = '#' + id;
    $(formatedId).val('');
}

function mapSelectData(v, i, a) {
    return { id: v.id, text: v.text }
}