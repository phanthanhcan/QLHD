$(function () {

    $('#KetQuaThamTra').change(function () {
        debugger;
        var value = $('#KetQuaThamTra').val();
        if (value == '0') {
            $('#SoPhieu').val('');
            $('#SoPhieu').attr('disabled', 'disabled')
            $('#ckMoLaiPhieu').attr('hidden', 'hidden')
            $('#dtpNgayGiaHan').attr('hidden', 'hidden')
        } else {
            $('#SoPhieu').removeAttr('disabled');
            $('#ckMoLaiPhieu').removeAttr('hidden')
            $('#dtpNgayGiaHan').removeAttr('hidden')
        }
    })

    $('#NgayThamTra').datepicker({
        dateformat: 'dd/mm/yy'
    });

    $('#NgayGiaHan').datepicker({
        dateformat: 'dd/mm/yy'
    });

    $('#IdNguoiThamTra').select2();
    $(".select2-container").css('width', '100%');

    $("#btn-saveTTKS").click(function () {
        debugger;
        var value = $('#KetQuaThamTra').val();
        var NgayThamTra = $('#NgayThamTra').val();
        var strNgayThamTra = '';
        var NgayGiaHan = $('#NgayGiaHan').val();
        var strNgayGiaHan = '';
        $('#errNgayThamTra').html(strNgayThamTra);
        $('#errNgayGiaHan').html(strNgayGiaHan);
        var strThongBao = '';
        $('#errThongBao').html(strThongBao);
        var KetQuaThamTra = $('#KetQuaThamTra').val();
        if (KetQuaThamTra == '1') {
            strThongBao = '<p class="help-block">Mức độ kiểm soát này là nhẹ, nên kết quả thẩm tra phải đạt. Nếu trường hợp không đạt, thì cần phải cập nhật lại mức độ là nặng</p>'
            $('#errThongBao').html(strThongBao);
            $('body,html').animate({ scrollTop: 0 }, 600);
            return false;
        }
        if (value == 0) {
            $('#NgayGiaHan').val('');
            $('#SoPhieu').val('');
        }
        if (NgayThamTra == '') {
            strNgayPhanTich = '<p class="help-block">Hãy chọn ngày thẩm tra</p>'
            $('#errNgayThamTra').html(strNgayPhanTich);
            $('body,html').animate({ scrollTop: 0 }, 600);
            return false;
        } else if (value == '1' && NgayGiaHan == '') {
            strNgayGiaHan = '<p class="help-block">Hãy chọn ngày gia hạn</p>'
            $('#errNgayGiaHan').html(strNgayGiaHan);
            $('body,html').animate({ scrollTop: 0 }, 600);
            return false;
        }
        else {
            $('#pgeThamTraKSKPH').submit();
            $('#myModal').modal('hide');
        }
    })
})