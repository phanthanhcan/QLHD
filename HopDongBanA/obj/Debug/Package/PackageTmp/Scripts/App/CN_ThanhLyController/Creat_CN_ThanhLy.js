$(document).ready(function(){
    $(document).on("click", "#btnThemThanhLy", function () {
        var IDHD = $("#IDHD").val();
        // checked
        var NgayTongNghiemThu = $("#NgayTongNghiemThu").val();
        var GiaTriQuyetToan = $("#GiaTriQuyetToan").val();
        var NgayThanhLy = $("#NgayThanhLy").val();
        var GiaTriThanhLy = $("#GiaTriThanhLy").val();
        var IsHoanThanh = $("#IsHoanThanh").bootstrapSwitch('state');
        
        var CN_ThanhLy = { "IDHD": IDHD, "NgayTongNghiemThu": NgayTongNghiemThu, "GiaTriQuyetToan": GiaTriQuyetToan, "NgayThanhLy": NgayThanhLy, "GiaTriThanhLy": GiaTriThanhLy, "IsHoanThanh": IsHoanThanh };
        $.ajax({
            url: "/CN_ThanhLy/Create_CN_ThanhLy",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(CN_ThanhLy),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data == "1") {
                    $("#NgayTongNghiemThu").attr("readonly", "readonly");
                    $("#GiaTriQuyetToan").attr("readonly", "readonly");
                    $("#NgayThanhLy").attr("readonly", "readonly");
                    $("#GiaTriThanhLy").attr("readonly", "readonly");
                    $("#IsHoanThanh").attr("readonly", "readonly");
                    document.getElementById("btnThemThanhLy").setAttribute("disabled", true);
                    $.notify({
                        title: '<strong>Thông báo</strong>:',
                        message: 'Cập nhật thành công'
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