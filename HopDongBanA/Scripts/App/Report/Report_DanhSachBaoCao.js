$(document).ready(function () {
    $(document).on("click", "#btnTheoDoiHopDong_NV", function () {

        var _MaNV = $('#liDMNV :selected').val();
        var _IDCT = $('#liDMCT :selected').val();
        var _TrangThai = $('#liTrangThai :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM01_TheoDoiHopDong_GetByNguonVon',
            type: "GET",
            data: {
                _MaNV: _MaNV,
                _IDCT: _IDCT,
                _TrangThai: _TrangThai
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

        var _IDCT = $('#liDMCT :selected').val();
        var _TenCongTrinh = $('#liDMCT :selected').text();
        var _TrangThai = $('#liTrangThai :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM01_TheoDoiHopDong_GetByCongTrinh',
            type: "GET",
            data: {
                _IDCT: _IDCT,
                _TrangThai: _TrangThai,
                _TenCongTrinh: _TenCongTrinh
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
    $(document).on("click", "#btnTheoDoiHopDong_LHD", function () {

        var _MaLHD = $('#liDMLHD :selected').val();
        var _TenLoaiHopDong = $('#liDMLHD :selected').text();
        var _TrangThai = $('#liTrangThai :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM01_TheoDoiHopDong_GetByLoaiHopDong',
            type: "GET",
            data: {
                _MaLHD: _MaLHD,
                _TrangThai: _TrangThai,
                _TenLoaiHopDong: _TenLoaiHopDong
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

    $(document).on("click", "#btnTheoDoiHopDong_DiaDiem", function () {

        var _MaDD = $('#liDMDD :selected').val();
        var _TenDD = $('#liDMDD :selected').text();
        var _TrangThai = $('#liTrangThai :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM11_TheoDoiHopDong_GetByDiaDiem',
            type: "GET",
            data: {
                _MaDD: _MaDD,
                _TrangThai: _TrangThai,
                _TenDD: _TenDD
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
    $(document).on("click", "#btnTheoDoiHopDong_NamKy", function () {

        var _NamKy = $('#liNamKy :selected').val();
        var _TrangThai = $('#liTrangThai :selected').val();
        Loading();
        $.ajax({
            url: '/Report/PrintBM12_TheoDoiHopDong_GetByNamKy',
            type: "GET",
            data: {
                _NamKy: _NamKy,
                _TrangThai: _TrangThai,
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

});