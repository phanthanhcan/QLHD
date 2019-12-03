$(function () {
    $('#NgayThamTra').datepicker({
        dateformat: 'dd/mm/yy'//,
        //autoclose: true,
        //locale: 'vi'
    });
    
    //$('#NgayThamTra').datepicker('update', new Date());

    $('#IdNguoiThamTra').select2();
    $(".select2-container").css('width', '100%');

    $('#NgayGiaHan').datepicker({
        dateformat: 'dd/mm/yy'//,
        //autoclose: true,
        //locale: 'vi'
    });

    debugger;
    $(document).ready(function () {
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



    $("#btn-save").click(function () {
        debugger;
        var value = $('#KetQuaThamTra').val();
        var NgayThamTra = $('#NgayThamTra').val();
        var strNgayThamTra = '';
        var NgayGiaHan = $('#NgayGiaHan').val();
        var strNgayGiaHan = '';
        $('#errNgayThamTra').html(strNgayThamTra);
        $('#errNgayGiaHan').html(strNgayGiaHan);
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
        }
        else {
            $('#pgeThamTraKSKPH').submit();
            $('#myModal').modal('hide');
        }
    })
})