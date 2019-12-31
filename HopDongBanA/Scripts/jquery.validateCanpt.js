/*!
 * jQuery Validation Plugin v1.19.1
 *
 * https://jqueryvalidation.org/
 *
 * Copyright (c) 2019 Jörn Zaefferer
 * Released under the MIT license
 */
$.validator.methods.range = function (value, element, param) {
    return true;
    var globalizedValue;
    if (element.className.includes('CheckNumber')) {
        globalizedValue = value.replace(/,/g, "");
    }

    if (element.className.includes('cDataPicker')) {
        var from = value.split("/");
        var sDate = from[2] + "/" + from[1] + "/" + from[0];
        globalizedValue = new Date(sDate);
        return true;
    }
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    var bool11 = this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    var bool = this.optional(element) || /^\d+(,\d{3})*(\.\d+)?$/.test(value);
    return bool;
}
$.validator.methods.date = function (value, element) {
    //var bool11 = this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    var from = value.split("/");
    var sDate = from[2] + "/" + from[1] + "/" + from[0];
    var bool = this.optional(element) || !/Invalid|NaN/.test(sDate);
    return bool;
}
