$(document).ready(function(){
    $(document).on("click", "#btnThemThanhLy", function () {

        var IDHD = $("#IDHD").val();
        var NgayThanhLy = $("#NgayThanhLy").val();
        var GiaTriThanhLy = $("#GiaTriThanhLy").val();
        var GiaTriQuyetToan = $("#GiaTriQuyetToan").val();
        var o_CN_ThanhLy = { "IDHD": IDHD, "NgayThanhLy": NgayThanhLy, "GiaTriThanhLy": GiaTriThanhLy, "GiaTriQuyetToan": GiaTriQuyetToan };

        $.ajax({
            url: "/CN_ThanhLy/Create_CN_ThanhLy",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(o_CN_ThanhLy),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data == "1") {
                    $("#NgayThanhLy").attr("readonly", "readonly");
                    $("#GiaTriThanhLy").attr("readonly", "readonly");
                    $("#GiaTriQuyetToan").attr("readonly", "readonly");
                    document.getElementById("btnThemThanhLy").setAttribute("disabled", true);
                    $.notify({
                        title: '<strong>Thông báo</strong>:',
                        message: 'Thêm mới thành công'
                    }, {
                        type: 'info'
                    });
                }
                else {
                    $.notify({
                        title: '<strong>Thông báo</strong>:',
                        message: data
                    }, {
                        type: 'danger'
                    });

                    console.log(data);
                }
            },
            error: function (xhr) {
                console.log("error: " + data + " status: " + status + " er:" + er);
            }
        });
    });

});