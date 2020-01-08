$(function () {
    $("#PhongBanThucHien").select2();

    $('#NgayPhanTich').datepicker({
        dateformat: 'dd/mm/yy'
    });
    
    $('#NgayPheDuyet').datepicker({
        dateformat: 'dd/mm/yyyy'
    });

    $('#ThoiHanHoanTat').datepicker({
        format: 'dd/mm/yy'
    });

    $('#ThoiHanHoanTatThucTe').datepicker({
        dateformat: 'dd/mm/yy'
    });

    $('#btnLuuHD').click(function () {
        var ngayphantich = $('#NgayPhanTich').val();
        var strNgayPhanTich = '';
        var ngaypheduyet = $('#NgayPheDuyet').val();
        var strNgayPheDuyet = '';
        var thoigianhoantat = $('#ThoiHanHoanTat').val();
        var strThoiGianHoanTat = '';
        var thoigianhoantatthucte = $('#ThoiHanHoanTatThucTe').val();
        var strThoiGianHoanTatThucTe = '';
        $('#errNgayPhanTich').html(strNgayPhanTich);
        $('#errNgayPheDuyet').html(strNgayPheDuyet);
        $('#errThoiHanHoanTat').html(strThoiGianHoanTat);
        $('#errThoiHanHoanTatThucTe').html(strThoiGianHoanTatThucTe);
        if (ngayphantich == '')
            strNgayPhanTich = '<p class="help-block">Hãy chọn ngày phân tích</p>'
        if (ngaypheduyet == '')
            strNgayPheDuyet = '<p class="help-block">Hãy chọn ngày phê duyệt</p>'
        if (thoigianhoantat == '')
            strThoiGianHoanTat = '<p class="help-block">Hãy chọn thời gian hoàn tất</p>'
        if (thoigianhoantatthucte == '')
            strThoiGianHoanTatThucTe = '<p class="help-block">Hãy chọn thời gian hoàn tất thực tế</p>'
        if (ngayphantich > ngaypheduyet)
            strNgayPheDuyet = '<p class="help-block">Ngày phê duyệt không được nhỏ hơn ngày phân tích</p>'
        if (thoigianhoantat < ngaypheduyet || thoigianhoantatthucte < ngaypheduyet) {
            strThoiGianHoanTat = '<p class="help-block">Hãy thời gian hoàn tất không được nhỏ hơn ngày phê duyệt</p>'
            strThoiGianHoanTatThucTe = '<p class="help-block">Hãy thời gian hoàn tất thực tế không được nhỏ hơn ngày phê duyệt</p>'
        }
        if (strNgayPhanTich == '' && strNgayPheDuyet == '' && strThoiGianHoanTat == '' && strThoiGianHoanTatThucTe == '') {
            $('#pgeCreateHDKP').submit();

        }
        else {
            if (strNgayPhanTich != '')
                $('#errNgayPhanTich').html(strNgayPhanTich);
            if (strNgayPheDuyet != '')
                $('#errNgayPheDuyet').html(strNgayPheDuyet);
            if (strThoiGianHoanTat != '')
                $('#errThoiHanHoanTat').html(strThoiGianHoanTat);
            if (strThoiGianHoanTatThucTe != '')
                $('#errThoiHanHoanTatThucTe').html(strThoiGianHoanTatThucTe);
            $('body,html').animate({ scrollTop: 0 }, 600);
            return false;
        }

    })
})
