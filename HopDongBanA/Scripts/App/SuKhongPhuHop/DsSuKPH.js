$(function () {
    // DataTable
    var table = $('#tblDsSuKPH').DataTable({
        responsive: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Vietnamese.json"
        },
        select: true,
        columnDefs: [
            { targets: [2, 3], width: '15%' }
        ],
        scrollY: "400px",
        scrollX: true,
        aaSorting: []
    });
    // Apply the search
    $('#cboTinhTrang').change(function () {
        var value = $('#cboTinhTrang').val();
        table.columns(0)
            .search(value)
            .draw();
    });


    $('#btnLapLai').click(function () {
        debugger;
        var idSelected = $("#tblDsSuKPH .selected").attr("id");
        if (idSelected != undefined) {
            Loading();
            $.ajax({
                method: 'POST',
                url: '/SuKhongPhuHops/PhieuLapLai',
                data: { id: idSelected },
                success: function (data) {
                    console.log(data);
                    if (data != "") {
                        window.location.reload();
                        LoadingHide();
                        $.notify({
                            // options
                            message: data
                        }, {
                            // settings
                            type: 'success'
                        });
                    } else {
                        LoadingHide();

                        $.notify({
                            // options
                            message: 'Tạo phiếu lỗi'
                        }, {
                            // settings
                            type: 'danger'
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

    $('#btnDel').click(function () {
        var idSelected = $("#tblDsSuKPH .selected").attr("id");
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
                        $('#tblDsSuKPH').DataTable().row('.selected').remove().draw(false);
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

    $('#btnUpdate').click(function () {
        debugger;
        var idSelected = $("#tblDsSuKPH .selected").attr("id");
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
})