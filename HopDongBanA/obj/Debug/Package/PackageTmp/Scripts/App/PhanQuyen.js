$(document).ready(function () {
    $(document).on("click", "#btnPhanQuyen", function () {
        var idNhom = $("#ListNhom tbody .selectItem ").attr("id");
        var ckbox = $('.checkBoxPQ');
        var arr = [];

        $.each(ckbox, function () {
            var check = $(this).is(":checked");
            var id = $(this).attr("data-id");
            var ele = { "check": check, "idChucNang": id };
            arr.push(ele);
        });

        var pqJson = {
            list: arr,
            idNhom: idNhom
        }

        $.ajax({
            url: "/HT_Nhom_ChucNang/XuLyPhanQuyen",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(pqJson),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data == "1") {
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

    $(document).on("click", "#btnPhanQuyenMenu", function () {
        var idNhom = $("#ListNhom tbody .selectItem ").attr("id");
        var ckbox = $('.checkBoxPQMenu');
        var arr = [];

        $.each(ckbox, function () {
            var check = $(this).is(":checked");
            var id = $(this).attr("data-id");
            var ele = { "check": check, "idChucNang": id };
            arr.push(ele);
        });

        var pqJson = {
            list: arr,
            idNhom: idNhom
        }

        $.ajax({
            url: "/HT_Nhom_ChucNang/XuLyPhanQuyen",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(pqJson),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data == "1") {
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

    //CanPT them cap nhat phan quyền hop phòng ban theo loai HD HT_PhongBan_LoaiHopDong
    //PhucPH sua
    $(document).on("click", "#btnPhanQuyenPhongBan", function () {
        var IDPB = $("#ListNhom tbody .selectItem ").attr("id");  // lay thong tin phongban
        var ckbox = $('.checkBoxLoaiHD'); // lay loai check bob theo class
        var arr = [];

        $.each(ckbox, function () {
            var check = $(this).is(":checked");
            var IDLoai = $(this).attr("data-id");
            var ele = { "check": check, "IDLoai": IDLoai};
            arr.push(ele);
        });

        var pqJson = {
            list: arr,
            IDPB: IDPB
        }

        $.ajax({
            url: "/HT_PhongBan_LoaiHopDong/XuLyPhanQuyenTheoLoaiHopDong",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(pqJson),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data == "1") {
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
