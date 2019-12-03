$(document).ready(function () {
    $(document).on("click", "#btnTheoDoiHopDong_NV", function () {

        var _MaNV = $('#liDMNV :selected').val();
        var _MaCT = $('#liDMCT :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM01_TheoDoiHopDong_GetByNguonVon',
            type: "GET",
            data: {
                _MaNV: _MaNV,
                _MaCT: _MaCT
            },
            success: function (data) {
                LoadingHide();
                $("#myModal").modal('show');
                $('#tableChiTiet').html(data);
            },
            error: function (xhr) {
                console.log("error:");
                LoadingHide();
            }
        });
    });
    $(document).on("click", "#btnTheoDoiHopDong_CT", function () {

        var _MaCT = $('#liDMCT :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM01_TheoDoiHopDong_GetByCongTrinh',
            type: "GET",
            data: { _MaCT: _MaCT },
            success: function (data) {
                LoadingHide();
                $("#myModal").modal('show');
                $('#tableChiTiet').html(data);
            },
            error: function (xhr) {
                console.log("error:" + xhr);
                LoadingHide();
            }
        });
    });
    $(document).on("click", "#btnTheoDoiHopDong_LHD", function () {

        var _MaLHD = $('#liDMLHD :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM02_TheoDoiHopDong_GetByLoaiHopDong',
            type: "GET",
            data: { _MaLHD: _MaLHD },
            success: function (data) {
                LoadingHide();
                $("#myModal").modal('show');
                $('#tableChiTiet').html(data);
            },
            error: function (xhr) {
                console.log("error:");
                LoadingHide();
            }
        });
    });
});