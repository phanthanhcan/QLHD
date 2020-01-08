
$('#TuNgay').datepicker({
    dateformat: 'dd/mm/yy'//,
    //autoclose: true,
    //locale: 'vi'
});
//$('#TuNgay').datepicker('update', new Date());

$('#DenNgay').datepicker({
    format: 'dd/mm/yy'//,
    //autoclose: true,
    //locale: 'vi'
});
//$('#DenNgay').datepicker('update', new Date());


//$('#NgayKiemSoat').datepicker({
//    format: 'dd/mm/yyyy',
//    autoclose: true,
//    locale: 'vi'
//});
//$('#NgayKiemSoat').datepicker('setDate', new Date());

$('#NgayXuLy').datepicker({
    format: 'dd/mm/yy'//,
    //autoclose: true,
    //locale: 'vi'
});



$(function () {
    debugger;
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                debugger;
                if (result == "") {
                    $('#myModal').modal('hide');
                    //Refresh
                    location.reload();
                } else {
                    $('#myModalContent').html(result);
                    //bindForm();
                }
            }
        });
        return false;
    });
}

$(function () {    

    

    //var i = 0;
    //$('#MucDo').change(function () {
    //    i = 1;
    //    var value = $('#MucDo').val();
    //    if (value == '0') {  //nặng
    //        $('#cankhacphuc').removeAttr('hidden');
    //        $('#cboCoHanhDongKhacPhuc').attr('disabled', 'disabled');
    //        $('#cboCoHanhDongKhacPhuc').val('1')
    //        $('#CoHanhDongKhacPhuc').val('1');
    //    } else if (value == '1') {  //nhẹ
    //        $('#cankhacphuc').removeAttr('hidden');
    //        $('#cboCoHanhDongKhacPhuc').removeAttr('disabled');
    //        $('#CoHanhDongKhacPhuc').val('0');
    //        $('#cboCoHanhDongKhacPhuc').val('0');
    //    }
    //    else {
    //        $('#cankhacphuc').attr('hidden', 'hidden');
    //    }
    //})

    //$('#CoHanhDongKhacPhuc').change(function () {
    //    var value = $('#CoHanhDongKhacPhuc').val();
    //    if (value == '0') {
    //        $('#hiden').val('chotphieu');
    //    } else {
    //        $('#hiden').val('khacphuc');
    //    }
    //})

    
});

//$('#btnDel').click(function () {
//    var idSelected = $("#example .selected").attr("id");
//    var url = $('#btnXoa').val();
//    if (idSelected != undefined) {
//        Loading();
//        $.ajax({
//            method: 'POST',
//            url: url,
//            data: { id: idSelected },
//            success: function (data) {
//                console.log(data);
//                if (data != "") {
//                    LoadingHide();
//                    $.notify({
//                        // options
//                        message: data
//                    }, {
//                        // settings
//                        type: 'danger'
//                    });
//                } else {
//                    $('#example').DataTable().row('.selected').remove().draw(false);
//                    LoadingHide();

//                    $.notify({
//                        // options
//                        message: 'Xóa thành công'
//                    }, {
//                        // settings
//                        type: 'info'
//                    });
//                }
//            }
//        })
//    } else {
//        $.notify({
//            // options
//            message: 'Bạn chưa chọn dòng nào'
//        }, {
//            // settings
//            type: 'danger'
//        });
//    }
//});



function Loading() {
    $("body").removeClass("modal-open");
    $(".modal-loading").remove();
    $(".modal-backdrop").remove();
    waitingDialog.show('Đang xử lý...');
}

function LoadingHide() {
    $("body").removeClass("modal-open");
    $(".modal-loading").remove();
    $(".modal-backdrop").remove();
    waitingDialog.hide();
};

window.setInterval(function () {
    $.get("/Home/Alive", function (data, usergrant) {
        //alert("Phiên làm việc vẫn sống");
    });
}, 180000);