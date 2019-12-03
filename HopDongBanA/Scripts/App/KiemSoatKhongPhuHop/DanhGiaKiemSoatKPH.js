$(function () {

    $('#NgayKiemSoat').datepicker({
        dateformat: 'dd/mm/yy'
    });
    //$('#NgayKiemSoat').datepicker('setDate', new Date());

    var i = 0;
    $('#MucDo').change(function () {
        i = 1;
        var value = $('#MucDo').val();
        if (value == '0') {  //nặng
            $('#cankhacphuc').removeAttr('hidden');
            $('#cboCoHanhDongKhacPhuc').attr('disabled', 'disabled');
            $('#cboCoHanhDongKhacPhuc').val('1')
            $('#CoHanhDongKhacPhuc').val('1');
        } else if (value == '1') {  //nhẹ
            $('#cankhacphuc').removeAttr('hidden');
            $('#cboCoHanhDongKhacPhuc').removeAttr('disabled');
            $('#CoHanhDongKhacPhuc').val('0');
            $('#cboCoHanhDongKhacPhuc').val('0');
        }
        else {
            $('#cankhacphuc').attr('hidden', 'hidden');
        }
    })

    $('#btnChotPhieu').click(function () {
        $('#hiden').val('chotphieu');
        $('#pgeCreateKSKPH').submit();
    })

    $('#btnKhacPhuc').click(function () {
        $('#hiden').val('khacphuc');
        $('#pgeCreateKSKPH').submit();
    })

    $('#btnLuuKSKPH').click(function () {
        var NgayKiemSoat = $('#NgayKiemSoat').val();
        var strNgayKiemSoat = '';
        $('#errNgayKiemSoat').html(strNgayKiemSoat);
        var valueMucDo = $('#MucDo').val();
        if (i != 1) {
            if (valueMucDo == '0') {
                $('#CoHanhDongKhacPhuc').val('1')
            } else if (valueMucDo == '2') {
                $('#CoHanhDongKhacPhuc').val('2')
            }
        }
        if (NgayKiemSoat == '')
            strNgayKiemSoat = '<p style="color:red">Hãy chọn ngày kiểm soát</p>';
        if (strNgayKiemSoat == '') {
            if (valueMucDo == '2') {
                $('#hiden').val('binhthuong');
                $('#pgeCreateKSKPH').submit();
                return;
            }
            var value = $('#CoHanhDongKhacPhuc').val();
            if (value == '0') {
                $('#hiden').val('chotphieu');
            } else if (value == '1') {
                $('#hiden').val('khacphuc');
            }
            $('#pgeCreateKSKPH').submit();
        }
        else {
            $('#errNgayKiemSoat').html(strNgayKiemSoat);
            $('body,html').animate({ scrollTop: 0 }, 600);
            return false;
        }
        i = 0;
    })
})