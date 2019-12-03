$(function () {
    // DataTable
    var table = $('#tblDsHanhDongKPH').DataTable({
        responsive: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Vietnamese.json"
        },
        select: true,
        columnDefs: [
            {
                targets: [0],
                visible: false
            } ,
            {
                targets: [3, 4],
                width: '15%'
            }
        ] ,
        scrollY: "400px",
        scrollX: true,
        aaSorting: []
    });
    // Apply the search
    $('#cboTinhTrang').change(function () {
        var value = $('#cboTinhTrang').val();
        if (value != "") { 
            var regex = '^' + value //+ '$'
            table.columns(0)
                .search(regex, true, false, true)
                .draw();
        } else {
            table.columns(0)
                .search(value) //, false, false)
                .draw();
        }

    });

    $('#btnTaoPhieu').click(function () {
        debugger;
        var id = $('.btnTaoPhieuMoi').val();
        Loading();
        $.ajax({
            url: '/HanhDongKhacPhucs/TaoPhieuMoi',
            data: { Id: id },
            type: 'POST',
            success: function (data) {
                if (data != "") {
                    $.notify({
                        // options
                        message: data
                    }, {
                        // settings
                        type: 'info'
                    });
                    window.location.href = '../KiemSoatSuKhongPhuHops/Index'
                }
                else {
                    $.notify({
                        // options
                        message: data
                    }, {
                        // settings
                        type: 'danger'
                    });
                    window.location.href = '../HanhDongKhacPhucs/Index'
                }
            },
            error: function (exception) { console.log(data); }
        });
    })

    $('#btnUpdate').click(function () {
        debugger;
        var idSelected = $("#tblDsHanhDongKPH .selected").attr("id");
        var url = $('#btnUpdate').val();
        if (idSelected != undefined) {
            var url = url//'@Url.Action("Edit", new { id = "_TOREPLACE_" })';
            url = url.replace('_TOREPLACE_', idSelected);
            window.location.href = url;
        } else {
            $.notify({
                // options
                message: 'Bạn chưa chọn dòng nào'
            }, {
                // settings
                type: 'danger'
            });
        }
    });

    $('#btnDel').click(function () {
        var idSelected = $("#tblDsHanhDongKPH .selected").attr("id");
        var url = $('#btnXoa').val();
        if (idSelected != undefined) {
            Loading();
            $.ajax({
                method: 'POST',
                url: url,
                data: { id: idSelected },
                success: function (data) {
                    console.log(data);
                    if (data != "") {
                        LoadingHide();
                        $.notify({
                            // options
                            message: data
                        }, {
                            // settings
                            type: 'danger'
                        });
                    } else {
                        $('#tblDsHanhDongKPH').DataTable().row('.selected').remove().draw(false);
                        LoadingHide();

                        $.notify({
                            // options
                            message: 'Xóa thành công'
                        }, {
                            // settings
                            type: 'info'
                        });
                    }
                }
            })
        } else {
            $.notify({
                // options
                message: 'Bạn chưa chọn dòng nào'
            }, {
                // settings
                type: 'danger'
            });
        }
    });

    debugger;
    var error = $("#error").val();
    if (error != "") {
        $.notify({
            message: error
        }, {
            type: 'danger'
        });
    }
})