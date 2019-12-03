function DonViOnChange() {
    var maDV = $("#cbDonVi").val();
    $.ajax({
        url: '/SuKhongPhuHops/DonViOnChange',
        type: "GET",
        dataType: "JSON",
        data: { maDonVi: maDV },
        success: function (data) {
            $("#IdNguoiPhatHien").html(""); // clear before appending new list
            console.log(data);
            $.each(data, function (i, nguoidung) {
                $("#IdNguoiPhatHien").append(
                    $('<option></option>').val(nguoidung.Id).html(nguoidung.Ten));
            })
        },
        error: function (xhr) {
            console.log("error: " + " status: " + status + " er:");
        }
    });
}

$(function () {
    //$('#NgayPhatHien').datepicker({
    //    format: 'dd/mm/yyyy',
    //    autoclose: true,
    //    locale: 'vi'
    //});
    //$('#NgayPhatHien').datepicker('update', new Date());

    $('#NgayPhatHien').datepicker({
        dateFormat: 'dd/mm/yy'
    });
    

    $('#btnSuKPH').click(function () {
        var ngayphathien = $('#NgayPhatHien').val();
        var strNgayPhatHien = '';
        var suKPH = $('#SuKph').val();
        var strSuKPH = '';
        $('#errNgayPhatHien').html(strNgayPhatHien);
        $('#errTenKPH').html(strSuKPH);
        if (ngayphathien == '')
            strNgayPhatHien = '<p class="help-block">(*)Hãy chọn ngày phát hiện</p>'
        if (suKPH == '')
            strSuKPH = '<p class="help-block">(*)Hãy nhập tên sự không phù hợp</p>'
        if (strNgayPhatHien == '' && strSuKPH == '') {
            $('#pgeCreateSuKPH').submit();
        }
        else {
            if (strNgayPhatHien != '')
                $('#errNgayPhatHien').html(strNgayPhatHien);
            if (strSuKPH != '')
                $('#errTenKPH').html(strSuKPH);
            $('body,html').animate({ scrollTop: 0 }, 600);
            return false;
        }
    })
})