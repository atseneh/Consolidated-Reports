function CustomDateRange(id) {
    $(id).daterangepicker(
        {
            showDropdowns: true,
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],

            },
            linkedCalendars: false,
            opens: 'right',
            startDate: moment(),
            endDate: moment()
        }
    )
}
function Loading(id)
{
    $(id).dxLoadPanel("instance").option("message", "Please Wait...");
    $(id).dxLoadPanel("instance").show();
}
function HideLoading(id)
{
    $(id).dxLoadPanel("instance").hide();
}
function DataTableWithExcelFunction(id) {
    $("." + id).DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": false,
        "buttons": ["excel"]
    }).buttons().container().appendTo('#' + id + '_wrapper .col-md-6:eq(0)');
}
function DataTableWithExcelFunctionResponsive(id) {
    $("#" + id).DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["excel"]
    }).buttons().container().appendTo('#' + id + '_wrapper .col-md-6:eq(0)');
}
function DataTableWithExcelNoPage(id) {
    $("#" + id).DataTable({
        "paging": false,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": false,
        "buttons": ["csv", "print", "excel", "colvis"]
    }).buttons().container().appendTo('#' + id + '_wrapper .col-md-6:eq(0)');
}
function DataTableFunctionNopagSearch(id) {
    $('.' + id).DataTable({
        "paging": false,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": false,
    });
}
function DataTableFunctionSearch(id) {
    $('.' + id).DataTable({
        "paging": false,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": false,
    });
}
function DataTableFunctionPaging(id) {
    $('.' + id).DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": false,
    });
}