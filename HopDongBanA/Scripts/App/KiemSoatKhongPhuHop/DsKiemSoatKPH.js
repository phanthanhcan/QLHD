$(function () {

    $('#NgayThamTra').datepicker({
        dateformat: 'dd/mm/yy'
    });

    // DataTable
    var table = $('#tblDsKiemSoat').DataTable({
        responsive: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Vietnamese.json"
        },
        select: true,
        columnDefs: [
            { targets: [2, 3], width: '15%' }
        ],
        scrollY: '400px',
        scrollX: true,
        scrollCollapse: true,
        aaSorting: []
    });
    // Apply the search
    $('#cboTinhTrang').change(function () {
        var value = $('#cboTinhTrang').val();
        if (value != "") {
            var regex = '^' + value + '$'
            table.columns(0)
                .search(regex, true) //, false, false)
                .draw();
        } else {
            table.columns(0)
                .search(value) //, false, false)
                .draw();
        }
    });


    $('#btnDel').click(function () {
        var idSelected = $("#tblDsKiemSoat .selected").attr("id");
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
                        $('#tblDsKiemSoat').DataTable().row('.selected').remove().draw(false);
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
        var idSelected = $("#tblDsKiemSoat .selected").attr("id");
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